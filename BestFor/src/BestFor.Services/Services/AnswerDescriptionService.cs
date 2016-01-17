using BestFor.Domain.Entities;
using BestFor.Data;
using BestFor.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    public class AnswerDescriptionService
    {
        private IRepository<AnswerDescription> _repository;

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
