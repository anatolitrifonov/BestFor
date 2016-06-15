using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Cache;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Implementation of flag service interface.
    /// Saves flags. Does not do anything else yet.
    /// </summary>
    public class FlagService : IFlagService
    {
        private ICacheManager _cacheManager;
        private IRepository<AnswerFlag> _answerFlagRepository;
        private IRepository<AnswerDescriptionFlag> _answerDescriptionFlagRepository;
        private ILogger _logger;

        public FlagService(
            ICacheManager cacheManager, 
            IRepository<AnswerFlag> answerFlagRepository,
            IRepository<AnswerDescriptionFlag> answerDescriptionFlagRepository, 
            ILoggerFactory loggerFactory)
        {
            _cacheManager = cacheManager;
            _answerFlagRepository = answerFlagRepository;
            _answerDescriptionFlagRepository = answerDescriptionFlagRepository;
            _logger = loggerFactory.CreateLogger<FlagService>();
        }

        /// <summary>
        /// Save answer flag
        /// </summary>
        /// <param name="answerFlag"></param>
        /// <returns></returns>
        public async Task<int> FlagAnswer(AnswerFlagDto answerFlag)
        {
            var answerFlagObject = new AnswerFlag();
            answerFlagObject.FromDto(answerFlag);

            _answerFlagRepository.Insert(answerFlagObject);

            await _answerFlagRepository.SaveChangesAsync();

            return answerFlagObject.Id;
        }

        /// <summary>
        /// Save answer description flag
        /// </summary>
        /// <param name="answerFlag"></param>
        /// <returns></returns>
        public async Task<int> FlagAnswerDescription(AnswerDescriptionFlagDto answerDescriptionFlag)
        {
            var answerDescriptionFlagObject = new AnswerDescriptionFlag();
            answerDescriptionFlagObject.FromDto(answerDescriptionFlag);

            _answerDescriptionFlagRepository.Insert(answerDescriptionFlagObject);

            await _answerDescriptionFlagRepository.SaveChangesAsync();

            return answerDescriptionFlagObject.Id;
        }
    }
}
