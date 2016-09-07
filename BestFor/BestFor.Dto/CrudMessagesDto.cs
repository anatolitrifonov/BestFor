using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    [ExcludeFromCodeCoverage]
    public class CrudMessagesDto
    {
        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }
    }
}
