using BestFor.Dto;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Interface to work with flags for answers and flags for answer descriptions
    /// </summary>
    public interface IFlagService
    {
        Task<int> FlagAnswer(AnswerFlagDto answerFlag);

        Task<int> FlagAnswerDescription(AnswerDescriptionFlagDto answerFlag);
    }
}
