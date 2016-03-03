namespace BestFor.Dto
{
    /// <summary>
    /// Model dto object used for adding answer description page
    /// </summary>
    public class AnswerDescriptionDto : BaseDto
    {
        public AnswerDto Answer { get; set; }

        public int AnswerId { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
