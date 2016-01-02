using System.Collections.Generic;
using System.Linq;

namespace BestFor.Dto
{
    public class SuggestionsDto : ErrorMessageDto
    {
        public IEnumerable<SuggestionDto> Suggestions { get; set; } = Enumerable.Empty<SuggestionDto>();
    }
}
