using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto.Account
{
    [ExcludeFromCodeCoverage]
    public class LoginViewDto
    {
        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// We will store the return url from request url here so that we can dynamically show reasons to login.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
