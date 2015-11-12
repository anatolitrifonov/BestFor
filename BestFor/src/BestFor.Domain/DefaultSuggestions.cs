using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Interfaces;

namespace BestFor.Domain
{
    public class DefaultSuggestions : ISuggestionDataSource
    {
        private static Dictionary<string, string> _data = new Dictionary<string, string>
        {
            ["Apple"] = "Apple",
            ["Microsoft"] = "Microsoft",
            ["IBM"] = "IBM"
        };

        public IEnumerable<Suggestion> FindSuggestions(string input)
        {
            return _data.Where(x => x.Key.StartsWith(input)).Select(x => new Suggestion() { Phrase = x.Value });
        }
    }
}
