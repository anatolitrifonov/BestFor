using System;
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
    public class ProfileViewDto : CrudMessagesDto
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public int NumberOfAnswers { get; set; }

        public int NumberOfDescriptions { get; set; }

        public int NumberOfVotes { get; set; }

        public int NumberOfFlags { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime JoinDate { get; set; }
    }
}
