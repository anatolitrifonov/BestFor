using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Represents a simple words suggestion for typeahead text box.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SuggestionDto : BaseDto
    {
        public SuggestionDto()
        {
        }
        public string Phrase { get; set; }
    }
}
