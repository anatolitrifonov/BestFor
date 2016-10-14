using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using BestFor.Dto.Account;
using BestFor.Domain.Interfaces;

namespace BestFor.Domain.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class

    public class ApplicationUser : IdentityUser, IDtoConvertable<ApplicationUserDto>
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


        [StringLength(1000, MinimumLength = 3)]
        public string CancellationReason { get; set; }

        #region IDtoConvertable implementation
        public ApplicationUserDto ToDto()
        {
            return new ApplicationUserDto()
            {
                UserId = Id,
                UserName = UserName,
                NumberOfAnswers = NumberOfAnswers,
                DisplayName = DisplayName
            };
        }

        public int FromDto(ApplicationUserDto dto)
        {
            Id = dto.UserId;
            UserName = dto.UserName;
            NumberOfAnswers = dto.NumberOfAnswers;

            return 1;
        }
        #endregion

    }
}
