using BestFor.Domain.Entities;
using BestFor.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Interface for answers service. Works with Answer object.
    /// </summary>
    public interface IAnswerService
    {
        /// <summary>
        /// Find the answers for the pair of suggestions
        /// </summary>
        /// <param name="leftWord"></param>
        /// <param name="rightWord"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindAnswers(string leftWord, string rightWord);

        /// <summary>
        /// Find the top N answers matching the left word
        /// </summary>
        /// <param name="leftWord"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindLeftAnswers(string leftWord);

        /// <summary>
        /// Find top <paramref name="count"/> answers matching the left word
        /// </summary>
        /// <param name="leftWord"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindLeftAnswers(string leftWord, int count);

        /// <summary>
        /// Find the top N answers matching the right word
        /// </summary>
        /// <param name="rightWord"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindRightAnswers(string rightWord);

        /// <summary>
        /// Find top <paramref name="count"/> answers matching the right word
        /// </summary>
        /// <param name="rightWord"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindRightAnswers(string rightWord, int count);

        /// <summary>
        /// Find the last ten answers for the pair of suggestions.
        /// </summary>
        /// <param name="leftWord"></param>
        /// <param name="rightWord"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindTopAnswers(string leftWord, string rightWord);

        /// <summary>
        /// Find answers trending today
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindAnswersTrendingToday();

        /// <summary>
        /// Find answers trending overall
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindAnswersTrendingOverall();

        /// <summary>
        /// Find exact answer by data
        /// </summary>
        /// <param name="leftWord"></param>
        /// <param name="rightWord"></param>
        /// <param name="phrase"></param>
        /// <returns></returns>
        Task<AnswerDto> FindExact(string leftWord, string rightWord, string phrase);

        /// <summary>
        /// Find an answer by its id.
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        Task<AnswerDto> FindById(int answerId);

        /// <summary>
        /// Add answer
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        Task<AddedAnswerDto> AddAnswer(AnswerDto answer);

        /// <summary>
        /// Edit answer
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        Task<AnswerDto> UpdateAnswer(AnswerDto answer);

        Task<int> HideAnswer(int answerId);
    }
}
