namespace BestFor.Dto
{
    /// <summary>
    /// Represents a simple words suggestion for typeahead text box.
    /// </summary>
    public class SuggestionDto : BaseDto
    {
        public SuggestionDto()
        {
        }
        public string Phrase { get; set; }
    }
}
