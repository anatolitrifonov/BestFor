using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Represents flag for the answer description
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AnswerDescriptionVoteDto : BaseDto
    {
        public AnswerDescriptionVoteDto()
        {
        }

        public int AnswerDescriptionId { get; set; }

        public string UserId { get; set; }
    }
}
