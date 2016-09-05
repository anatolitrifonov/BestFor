using BestFor.Domain.Entities;
using BestFor.Dto;
using System.Collections.Generic;

namespace BestFor.Models
{
    public class AdminUserDescriptionsViewModel
    {
        public ApplicationUser User { get; set; }

        public IEnumerable<AnswerDescriptionDto> AnswerDescriptions { get; set; }
    }
}
