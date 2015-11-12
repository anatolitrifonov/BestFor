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
        private static ISuggestionDataSource _suggestiosDataSource = new DefaultSuggestions();

        public static ISuggestionDataSource GetSuggestionsDataSource()
        {
            return _suggestiosDataSource;
        }
    }
}
