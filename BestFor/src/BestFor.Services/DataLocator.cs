using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Interfaces;
using BestFor.Domain;

namespace BestFor.Services
{
    public class DataLocator
    {
        private static ISuggestionDataSource _suggestiosDataSource = new ShortSuggestions();
        private static IAnswerDataSource _answersDataSource = new RandomAnswers();

        public static ISuggestionDataSource GetSuggestionsDataSource()
        {
            return _suggestiosDataSource;
        }

        public static IAnswerDataSource GetAnswersDataSource()
        {
            return _answersDataSource;
        }
    }
}
