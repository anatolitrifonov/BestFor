namespace BestFor.Dto
{
    /// <summary>
    /// Represents a simple words suggestion for typeahead text box.
    /// </summary>
    public class AnswerDto : BaseDto
    {
        public AnswerDto()
        {
        }

        public string LeftWord { get; set; }

        public string RightWord { get; set; }

        public string Phrase { get; set; }

        public int Count { get; set; }

    }
}
