using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Represents flag for the answer
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AnswerFlagDto : BaseDto
    {
        public AnswerFlagDto()
        {
        }

        public int AnswerId { get; set; }

        public string Reason { get; set; }

        public string UserId { get; set; }
    }
}
