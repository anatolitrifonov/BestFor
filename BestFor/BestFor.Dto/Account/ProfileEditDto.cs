using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto.Account
{
    /// <summary>
    /// Model/Dto object for user's manage account page.
    /// </summary>
    /// <remarks>
    /// Data annotations do not quite work yet.
    /// Plus I still have no idea how to do the localized validation without setting the thread locale.
    /// I do not want to set the thread locale.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class ProfileEditDto : CrudMessagesDto
    {
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "*", MinimumLength = 6)] // The {0} must be at least {2} characters long.
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "*", MinimumLength = 6)] // The {0} must be at least {2} characters long.
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
    }
}
