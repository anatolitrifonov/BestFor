using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BestFor.Dto;
using BestFor.Domain;
using BestFor.Domain.Entities;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Interface for suggestions service
    /// </summary>
    public interface IAnswerService
    {
        /// <summary>
        /// search the answer for the pair of suggestions
        /// </summary>
        /// <param name="leftWord"></param>
        /// <param name="rightWord"></param>
        /// <returns></returns>
        IEnumerable<AnswerDto> FindAnswers(string leftWord, string rightWord);

        IEnumerable<AnswerDto> FindTopAnswers(string leftWord, string rightWord);

        AnswerDto FindExact(string leftWord, string rightWord, string phrase);

        Task<Answer> AddAnswer(AnswerDto answer);
    }
}
