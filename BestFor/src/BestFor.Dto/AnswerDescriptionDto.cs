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

        /// <summary>
        /// Userid for the user that added this description.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Username for the user that added this description.
        /// </summary>
        public string UserName { get; set; }
    }
}
