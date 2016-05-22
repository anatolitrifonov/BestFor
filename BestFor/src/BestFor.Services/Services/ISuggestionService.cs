using System.Collections.Generic;
using System.Threading.Tasks;
using BestFor.Dto;
using BestFor.Domain.Entities;

namespace BestFor.Services.Services
{
    /// <summary>
    /// Interface for suggestions service
    /// </summary>
    public interface ISuggestionService
    {
        /// <summary>
        /// Basic search for word suggestion
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SuggestionDto>> FindSuggestions(string input);

        Task<Suggestion> AddSuggestion(SuggestionDto suggestion);
    }
}
