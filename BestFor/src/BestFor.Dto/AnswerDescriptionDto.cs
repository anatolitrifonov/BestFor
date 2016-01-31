namespace BestFor.Dto
{
    public class AnswerDescriptionDto : BaseDto
    {
        public AnswerDto Answer { get; set; }

        public int AnswerId { get; set; }

        public string Description { get; set; }
    }
}
