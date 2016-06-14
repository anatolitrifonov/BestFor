namespace BestFor.Dto
{
    /// <summary>
    /// Represents flag for the answer description
    /// </summary>
    public class AnswerDescriptionFlagDto : BaseDto
    {
        public AnswerDescriptionFlagDto()
        {
        }

        public int AnswerDescriptionId { get; set; }

        public string Reason { get; set; }

        public string UserId { get; set; }
    }
}
