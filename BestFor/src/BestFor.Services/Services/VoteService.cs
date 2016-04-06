using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Cache;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

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

            return answerDescriptionVoteObject.Id;
        }
    }
}
