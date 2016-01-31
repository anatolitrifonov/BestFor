using BestFor.Domain.Entities;
using BestFor.Data;
using BestFor.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Service to work with answer descriptions.
    /// </summary>
    public class AnswerDescriptionService : IAnswerDescriptionService
    {
        /// <summary>
        /// Save repository between constractor and methods.
        /// </summary>
        private IRepository<AnswerDescription> _repository;

        /// <summary>
        /// Repository is injected in startup
        /// </summary>
        /// <param name="repository"></param>
        public AnswerDescriptionService(IRepository<AnswerDescription> repository)
        {
            _repository = repository;
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
    }
}
