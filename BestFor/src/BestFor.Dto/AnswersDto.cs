using System.Collections.Generic;
using System.Linq;

namespace BestFor.Dto
{
    public class AnswersDto : CrudMessagesDto
    {
        public IEnumerable<AnswerDto> Answers { get; set; } = Enumerable.Empty<AnswerDto>();
    }
}
