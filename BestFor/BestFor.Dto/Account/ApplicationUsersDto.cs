using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto.Account
{
    /// <summary>
    /// Collection of users.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApplicationUsersDto : CrudMessagesDto
    {
        public List<ApplicationUserDto> Users { get; set; } = new List<ApplicationUserDto>();
    }
}
