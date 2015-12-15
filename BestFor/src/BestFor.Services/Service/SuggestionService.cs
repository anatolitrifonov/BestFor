using BestFor.Dto;
using System.Collections.Generic;
using System.Linq;

namespace BestFor.Services.Service
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    /// <summary>
    /// Suggestion service implementation
    /// </summary>
    public class SuggestionService: ISuggestionService
    {
        // private static 
        public SuggestionService()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<SuggestionDto> FindSuggestions(string input)
        {
            // Find data source for suggestions
            // Call its find suggestions method
            // Ask results to convert itselft to Dto 
            return DataLocator.GetSuggestionsDataSource().FindSuggestions(input).Select(x => x.ToDto());
        }

    }
}
