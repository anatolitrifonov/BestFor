using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Domain.Interfaces
{
    public interface ISuggestionDataSource
    {
        IEnumerable<Suggestion> FindSuggestions(string input);
    }
}
