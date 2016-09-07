using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto.Account
{
    [ExcludeFromCodeCoverage]
    public class ForgotPasswordViewDto
    {
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
