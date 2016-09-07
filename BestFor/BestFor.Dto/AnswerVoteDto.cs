using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Represents flag for the answer
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AnswerVoteDto : BaseDto
    {
        public AnswerVoteDto()
        {
        }

        public int AnswerId { get; set; }

        public string UserId { get; set; }
    }
}
