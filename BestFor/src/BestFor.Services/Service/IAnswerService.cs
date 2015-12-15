using System.Collections.Generic;
using BestFor.Dto;
using BestFor.Domain;
using BestFor.Domain.Entities;

namespace BestFor.Services.Service
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

        Answer AddAnswer(AnswerDto answer);
    }
}
