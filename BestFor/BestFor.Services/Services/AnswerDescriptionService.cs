using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Service to work with answer descriptions.
    /// 
    /// Answer Descriptions are stored in cache double index first by answerId then by id
    /// </summary>
    public class AnswerDescriptionService : IAnswerDescriptionService
    {
        /// <summary>
        /// Save repository between constractor and methods. Plus injection.
        /// </summary>
        private ICacheManager _cacheManager;
        private IAnswerDescriptionRepository _repository;
        private ILogger _logger;

        /// <summary>
        /// Repository is injected in startup
        /// </summary>
        /// <param name="repository"></param>
        public AnswerDescriptionService(ICacheManager cacheManager, IAnswerDescriptionRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _cacheManager = cacheManager;
            _logger = loggerFactory.CreateLogger<AnswerService>();
            _logger.LogInformation("created AnswerDescriptionService");
        }

        #region IAnswerDescriptionService implementation
        /// <summary>
        /// Add answer description.
        /// </summary>
        /// <param name="answerDescription"></param>
        /// <returns></returns>
        public async Task<AnswerDescription> AddAnswerDescription(AnswerDescriptionDto answerDescription)
        {
            var answerDescriptionObject = new AnswerDescription();
            answerDescriptionObject.FromDto(answerDescription);

            // Save to database
            _repository.Insert(answerDescriptionObject);
            await _repository.SaveChangesAsync();

            // Add to cache.
            var cachedData = GetCachedData();
            cachedData.Insert(answerDescriptionObject);

            return answerDescriptionObject;
        }

        /// <summary>
        /// Find answer descriptions by answer id
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDescriptionDto>> FindByAnswerId(int answerId)
        {
            // return blank list if invalid answerId
            if (answerId == 0) return await Task.FromResult(Enumerable.Empty<AnswerDescriptionDto>());
            // Get cache
            var cachedData = GetCachedData();
            // Get data
            var data = cachedData.Find(answerId.ToString());
            if (data == null) return null;

            // Return data
            return data.Select(x => x.ToDto());
            // Commenting straig get from repo
            // Search for all descriptions for a given answer
            // return _repository.Queryable().Where(x => x.AnswerId == answerId).Select(x => x.ToDto());
        }

        /// <summary>
        /// Load answer descriptions for a given answer.
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDescriptionDto>> FindDirectByAnswerId(int answerId)
        {
            // return blank list if invalid answerId
            if (answerId == 0) return await Task.FromResult(Enumerable.Empty<AnswerDescriptionDto>());

            var data = _repository.FindByAnswerId(answerId);

            // Return data
            return data.Select(x => x.ToDto());
        }

        /// <summary>
        /// Find answer descriptions by answer description id
        /// </summary>
        /// <param name="answerDescriptionId"></param>
        /// <returns></returns>
        public async Task<AnswerDescriptionDto> FindByAnswerDescriptionId(int answerDescriptionId)
        {
            // return blank list if invalid answerId
            if (answerDescriptionId == 0) return await Task.FromResult<AnswerDescriptionDto>(new AnswerDescriptionDto());
            // Get cache
            var cachedData = GetCachedData();
            // Get data
            var data = await cachedData.FindExactById(answerDescriptionId);
            if (data == null) return null;

            // Return data
            return data.ToDto();
        }

        /// <summary>
        /// Find all answers with no user going directly to the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDescriptionDto>> FindDirectBlank()
        {
            var data = _repository.FindAnswerDescriptionsWithNoUser();
            return await Task.FromResult(data.Select(x => x.ToDto()));
        }

        /// <summary>
        /// Find all answer descriptions for a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AnswerDescriptionDto>> FindDirectByUserId(string userId)
        {
            var data = _repository.FindByUserId(userId);
            return await Task.FromResult(data.Select(x => x.ToDto()));
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get data from cache or initialize if empty
        /// </summary>
        /// <returns></returns>
        private KeyIndexedDataSource<AnswerDescription> GetCachedData()
        {
            object data = _cacheManager.Get(CacheConstants.CACHE_KEY_ANSWER_DESCRIPTIONS_DATA);
            if (data == null)
            {
                var dataSource = new KeyIndexedDataSource<AnswerDescription>();
                dataSource.Initialize(_repository.Active());
                _cacheManager.Add(CacheConstants.CACHE_KEY_ANSWER_DESCRIPTIONS_DATA, dataSource);
                return dataSource;
            }
            return (KeyIndexedDataSource<AnswerDescription>)data;
        }
        #endregion
    }
}
