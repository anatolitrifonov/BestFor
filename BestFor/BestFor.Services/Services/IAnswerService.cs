using BestFor.Dto.Account;
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
        Task<AnswerDto> FindByAnswerId(int answerId);

        /// <summary>
        /// Find all answers for user going directly to the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindDirectByUserId(string userId);

        /// <summary>
        /// Find all answers with no user going directly to the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindDirectBlank();

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

        /// <summary>
        /// Return top N of all answers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindAllAnswers();

        /// <summary>
        /// Find top <paramref name="count"/> answers
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindAllAnswers(int count);

        /// <summary>
        /// Return top N of Last answers ordered by date
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindLastAnswers();

        /// <summary>
        /// Find top <paramref name="count"/> answers sorted by date
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindLastAnswers(int count);

        /// <summary>
        /// Return top N of Last answers ordered by date desc by keyword
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindLastAnswers(string searchPhrase);

        /// <summary>
        /// Return top N of Last answers ordered by date desc by keyword
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnswerDto>> FindLastAnswers(int count, string searchPhrase);

        /// <summary>
        /// Find top N users answer posters
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationUserDto>> FindTopPosterIds();

    }
}
