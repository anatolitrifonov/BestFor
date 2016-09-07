using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Represents flag for the answer description
    /// </summary>
    [ExcludeFromCodeCoverage]
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
