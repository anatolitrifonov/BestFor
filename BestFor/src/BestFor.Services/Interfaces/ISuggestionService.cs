using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Dto;

namespace BestFor.Services.Interfaces
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
        IEnumerable<SuggestionDto> FindSuggestions(string input);
    }
}
