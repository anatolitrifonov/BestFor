using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Dto.Account
{
    public class ForgotPasswordViewDto
    {
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
