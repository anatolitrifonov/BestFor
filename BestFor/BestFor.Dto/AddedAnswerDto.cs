using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    [ExcludeFromCodeCoverage]
    public class AddedAnswerDto : CrudMessagesDto
    {
        public AnswerDto Answer { get; set; }
    }
}
