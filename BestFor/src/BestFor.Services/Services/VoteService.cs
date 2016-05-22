﻿using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Implementation of vote service interface.
    /// Saves votes. Does not do anything else yet.
    /// </summary>
    /// <remarks>Reason for not combining this with answer service is so that it can be tested separately
    /// and not to load the classes to much</remarks>
    public class VoteService : IVoteService
    {
        private ICacheManager _cacheManager;
        private IRepository<AnswerVote> _answerVoteRepository;
        private IRepository<AnswerDescriptionVote> _answerDescriptionVoteRepository;
        private ILogger _logger;

        public VoteService(
            ICacheManager cacheManager, 
            IRepository<AnswerVote> answerVoteRepository,
            IRepository<AnswerDescriptionVote> answerDescriptionVoteRepository, 
            ILoggerFactory loggerFactory)
        {
            _cacheManager = cacheManager;
            _answerVoteRepository = answerVoteRepository;
            _answerDescriptionVoteRepository = answerDescriptionVoteRepository;
            _logger = loggerFactory.CreateLogger<VoteService>();
        }

        /// <summary>
        /// Save answer vote
        /// </summary>
        /// <param name="voteVote"></param>
        /// <returns></returns>
        public async Task<int> VoteAnswer(AnswerVoteDto answerVote)
        {
            if (answerVote == null)
                throw new ServicesException("Null parameter VoteService.VoteAnswer(answerVote)");

            if (answerVote.AnswerId <= 0)
                throw new ServicesException("Unexpected AnswerId in VoteService.VoteAnswer(answerVote)");

            if (answerVote.UserId == null)
                throw new ServicesException("Unexpected UserId in VoteService.VoteAnswer(answerVote)");

            // Find if vote is already there.
            var existingVote = _answerVoteRepository.Queryable()
                .FirstOrDefault(x => x.UserId == answerVote.UserId && x.AnswerId == answerVote.AnswerId);
            // Do not re-add existing vote.
            if (existingVote != null) return existingVote.Id;

            // Add new vote
            var answerVoteObject = new AnswerVote();
            answerVoteObject.FromDto(answerVote);

            _answerVoteRepository.Insert(answerVoteObject);
            await _answerVoteRepository.SaveChangesAsync();

            // Add to cache.
            var cachedData = await GetVotesCachedData();
            await cachedData.Insert(answerVoteObject);

            return answerVoteObject.Id;
        }

        /// <summary>
        /// Save answer description Vote
        /// </summary>
        /// <param name="answerVote"></param>
        /// <returns></returns>
        public async Task<int> VoteAnswerDescription(AnswerDescriptionVoteDto answerDescriptionVote)
        {
            if (answerDescriptionVote == null)
                throw new ServicesException("Null parameter VoteService.VoteAnswerDescription(answerDescriptionVote)");

            if (answerDescriptionVote.AnswerDescriptionId <= 0)
                throw new ServicesException("Unexpected AnswerDescriptionId in VoteService.VoteAnswerDescription(answerDescriptionVote)");

            if (answerDescriptionVote.UserId == null)
                throw new ServicesException("Unexpected UserId in VoteService.VoteAnswerDescription(answerDescriptionVote)");

            // Find if vote is already there.
            var existingVote = _answerDescriptionVoteRepository.Queryable()
                .FirstOrDefault(x => x.UserId == answerDescriptionVote.UserId && x.AnswerDescriptionId == answerDescriptionVote.AnswerDescriptionId);
            // Do not re-add existing vote.
            if (existingVote != null) return existingVote.Id;

            // Add new vote
            var answerDescriptionVoteObject = new AnswerDescriptionVote();
            answerDescriptionVoteObject.FromDto(answerDescriptionVote);

            _answerDescriptionVoteRepository.Insert(answerDescriptionVoteObject);
            await _answerDescriptionVoteRepository.SaveChangesAsync();

            // Add to cache.
            var cachedData = await GetVoteDescriptionsCachedData();
            await cachedData.Insert(answerDescriptionVoteObject);

            return answerDescriptionVoteObject.Id;
        }

        public async Task<int> CountAnswerVotes(int answerId)
        {
            if (answerId <= 0) return 0;

            // Get cache.
            var cachedData = await GetVotesCachedData();
            // Get count from cache.
            var data = await cachedData.Find(answerId.ToString());
            if (data == null) return 0;
            return data.Count();

            // int count = _answerVoteRepository.Queryable().Count(x => x.AnswerId == answerId);
            // return await Task.FromResult<int>(count);
        }

        #region Private Methods
        /// <summary>
        /// Get votes data from cache or initialize if empty
        /// </summary>
        /// <returns></returns>
        private async Task<KeyIndexedDataSource<AnswerVote>> GetVotesCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_VOTES_DATA);
            if (data == null)
            {
                var dataSource = new KeyIndexedDataSource<AnswerVote>();
                await dataSource.Initialize(_answerVoteRepository);
                _cacheManager.Add(CacheConstants.CACHE_KEY_VOTES_DATA, dataSource);
                return dataSource;
            }
            return (KeyIndexedDataSource<AnswerVote>)data;
        }

        /// <summary>
        /// Get vote descriptions data from cache or initialize if empty
        /// </summary>
        /// <returns></returns>
        private async Task<KeyIndexedDataSource<AnswerDescriptionVote>> GetVoteDescriptionsCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_DESCRIPTION_VOTES_DATA);
            if (data == null)
            {
                var dataSource = new KeyIndexedDataSource<AnswerDescriptionVote>();
                await dataSource.Initialize(_answerDescriptionVoteRepository);
                _cacheManager.Add(CacheConstants.CACHE_KEY_DESCRIPTION_VOTES_DATA, dataSource);
                return dataSource;
            }
            return (KeyIndexedDataSource<AnswerDescriptionVote>)data;
        }
        #endregion
    }
}
