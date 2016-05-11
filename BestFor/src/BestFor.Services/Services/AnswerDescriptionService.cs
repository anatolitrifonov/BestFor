using BestFor.Domain.Entities;
using BestFor.Data;
using BestFor.Dto;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Service to work with answer descriptions.
    /// </summary>
    public class AnswerDescriptionService : IAnswerDescriptionService
    {
        /// <summary>
        /// Save repository between constractor and methods. Plus injection.
        /// </summary>
        private IAnswerDescriptionRepository _repository;
        private ILogger _logger;

        /// <summary>
        /// Repository is injected in startup
        /// </summary>
        /// <param name="repository"></param>
        public AnswerDescriptionService(IAnswerDescriptionRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<AnswerService>();
            _logger.LogInformation("created AnswerDescriptionService");
        }

        /// <summary>
        /// Add answer description.
        /// </summary>
        /// <param name="answerDescription"></param>
        /// <returns></returns>
        public async Task<AnswerDescription> AddAnswerDescription(AnswerDescriptionDto answerDescription)
        {
            var answerDescriptionObject = new AnswerDescription();
            answerDescriptionObject.FromDto(answerDescription);

            _repository.Insert(answerDescriptionObject);

            await _repository.SaveChangesAsync();

            return answerDescriptionObject;
        }

        public IEnumerable<AnswerDescriptionDto> FindByAnswerId(int answerId)
        {
            // return blank list if invalid answerId
            if (answerId == 0) return Enumerable.Empty<AnswerDescriptionDto>();
            // Search for all descriptions for a given answer
            return _repository.Queryable().Where(x => x.AnswerId == answerId).Select(x => x.ToDto());
        }
    }
}
