using System.Collections.Generic;
using System.Linq;

namespace BestFor.Dto
{
    public class AnswersDto : ErrorMessageDto
    {
        public IEnumerable<AnswerDto> Answers { get; set; } = Enumerable.Empty<AnswerDto>();
    }
}
