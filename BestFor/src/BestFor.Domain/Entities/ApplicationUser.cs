using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BestFor.Domain.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class

    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Optional display name. Username will be displayed when blank.
        /// </summary>
        [StringLength(100, ErrorMessage = "*", MinimumLength = 6)] // The {0} must be at least {2} characters long.
        public string DisplayName { get; set; }
    }
}
