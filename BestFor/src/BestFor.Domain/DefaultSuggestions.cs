using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Interfaces;
using BestFor.Domain.Entities;

namespace BestFor.Domain
{
    /// <summary>
    /// Implements the default storage of word phrase suggestions
    /// </summary>
    public class DefaultSuggestions : ISuggestionDataSource
    {
        private static Dictionary<string, string> _data = new Dictionary<string, string>
        {
            ["Apple"] = "Apple",
            ["Microsoft"] = "Microsoft",
            ["IBM"] = "IBM"
        };

        /// <summary>
        /// Implmentation of suggestion search based on word start
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<Suggestion> FindSuggestions(string input)
        {
            // Search default dictionary and return the list of suggestions
            return _data.Where(x => x.Key.StartsWith(input)).Select(x => new Suggestion() { Phrase = x.Value });
        }
    }
}
