using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto.Contact
{
    /// <summary>
    /// Model used on contact us page
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ContactUsDto
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "*")]
        public string Content { get; set; }

        /// <summary>
        /// User will have to type something to check if he is human.
        /// </summary>
        //[Required(ErrorMessage = "*")]
        public string Check { get; set; }

        [EmailAddress(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
