using System;
using BestFor.Dto;
using BestFor.Domain;
using BestFor.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BestFor.Services
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    /// <summary>
    /// Suggestion service implementation
    /// </summary>
    public class AnswerService : IAnswerService
    {
        // private static 
        public AnswerService()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<AnswerDto> FindAnswers(string leftWord, string rightWord)
        {
            // Find data source for suggestions
            // Call its find suggestions method
            // Ask results to convert itselft to Dto 
            return DataLocator.GetAnswersDataSource().FindAnswers(leftWord, rightWord).Select(x => x.ToDto());
        }

        public Guid AddAnswer(AnswerDto answer)
        {
            var answerObject = new Answer();
            answerObject.FromDto(answer);
            return DataLocator.GetAnswersDataSource().AddAnswer(answerObject);
        }

    }
}
