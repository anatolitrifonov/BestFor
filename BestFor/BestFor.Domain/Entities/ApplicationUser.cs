using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

        public int NumberOfAnswers { get; set; }

        public int NumberOfDescriptions { get; set; }

        public int NumberOfVotes { get; set; }

        public int NumberOfFlags { get; set; }

        public int NumberOfComments { get; set; }

        /// <summary>
        /// User's favorite opinions category
        /// </summary>
        public string FavoriteCategory { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime DateCancelled { get; set; }

        public bool IsCancelled { get; set; }
    }
}
