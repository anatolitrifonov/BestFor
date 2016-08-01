using BestFor.Domain.Entities;
using BestFor.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Interface for answer descriptions service. Works with AnswerDescription object.
    /// </summary>
    public interface IAnswerDescriptionService
    {
        /// <summary>
        /// Find all descriptions of a given answer
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDescriptionDto>> FindByAnswerId(int answerId);

        /// <summary>
        /// Add AnswerDescription
        /// </summary>
        /// <param name="answerDescription"></param>
        /// <returns></returns>
        Task<AnswerDescription> AddAnswerDescription(AnswerDescriptionDto answerDescription);

        /// <summary>
        /// Find a given description from its id.
        /// </summary>
        /// <param name="answerDescriptionId"></param>
        /// <returns></returns>
        Task<AnswerDescriptionDto> FindByAnswerDescriptionId(int answerDescriptionId);
    }
}
