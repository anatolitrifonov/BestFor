using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BestFor.Dto
{
    /// <summary>
    /// Model dto object used for displaying full answer related information.
    /// Used on Home/MyContent view.
    /// </summary>
    [ExcludeFromCodeCoverage]
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

        /// <summary>
        /// Shows additional message on the content page. Example "Done voiting" or "thanks for voting".
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// This setting has different values depending on controller
        /// </summary>
        public bool EnableFacebookSharing { get; set; } = false;

        /// <summary>
        /// Link to this page
        /// </summary>
        public string ThisAnswerLink { get; set; }

        /// <summary>
        /// Link to this page with domain name
        /// </summary>
        public string ThisAnswerFullLink { get; set; }

        /// <summary>
        /// Escaped link to this page with domain name
        /// </summary>
        public string ThisAnswerFullLinkEscaped { get; set; }

        /// <summary>
        /// Text for this answer "best something for  something is something"
        /// </summary>
        public string ThisAnswerText { get; set; }

        /// <summary>
        /// React controls rendered on the page will show debug messages if URL parameter is set to true
        /// </summary>
        public bool DebugReactControls { get; set; } = false;
    }
}
