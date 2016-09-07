using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace BestFor.Dto
{
    [ExcludeFromCodeCoverage]
    public class AnswersDto : CrudMessagesDto
    {
        public IEnumerable<AnswerDto> Answers { get; set; } = Enumerable.Empty<AnswerDto>();

        /// <summary>
        /// When true this is search result on the left words
        /// When false this is search result on the right word
        /// </summary>
        public bool IsLeft { get; set; } = true;

        public string SearchKeyword { get; set; }
    }
}
