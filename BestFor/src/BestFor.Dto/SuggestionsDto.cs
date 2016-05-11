using System.Collections.Generic;
using System.Linq;

namespace BestFor.Dto
{
    public class SuggestionsDto : CrudMessagesDto
    {
        public IEnumerable<SuggestionDto> Suggestions { get; set; } = Enumerable.Empty<SuggestionDto>();
    }
}
