using System.Collections.Generic;
using System.Linq;

namespace BestFor.Dto
{
    /// <summary>
    /// Model dto object used for displaying full answer related information
    /// </summary>
    public class AnswerDetailsDto : CrudMessagesDto
    {
        public AnswerDto Answer { get; set; }

        public IEnumerable<AnswerDescriptionDto> Descriptions { get; set; } = Enumerable.Empty<AnswerDescriptionDto>();

        public CommonStringsDto CommonStrings { get; set; }

        /// <summary>
        /// Display Name of the user that added the answer
        /// </summary>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Number of votes that this answer got.
        /// </summary>
        public int NumberVotes { get; set; }
    }
}
