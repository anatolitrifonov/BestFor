using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    [ExcludeFromCodeCoverage]
    public class SuggestionsDto : CrudMessagesDto
    {
        public IEnumerable<SuggestionDto> Suggestions { get; set; } = Enumerable.Empty<SuggestionDto>();
    }
}
