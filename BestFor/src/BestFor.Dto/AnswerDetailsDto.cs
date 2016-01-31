using System.Collections.Generic;
using System.Linq;

namespace BestFor.Dto
{
    /// <summary>
    /// Model dto object used for displaying full answer related information
    /// </summary>
    public class AnswerDetailsDto
    {
        public AnswerDto Answer { get; set; }

        public IEnumerable<AnswerDescriptionDto> Descriptions { get; set; } = Enumerable.Empty<AnswerDescriptionDto>();
    }
}
