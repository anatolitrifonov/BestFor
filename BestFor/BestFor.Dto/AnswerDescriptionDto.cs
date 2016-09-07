using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Model dto object used for adding answer description page
    /// </summary>
    [ExcludeFromCodeCoverage]
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
        /// Display Name of the user that added the answer
        /// </summary>
        public string UserDisplayName { get; set; }
    }
}
