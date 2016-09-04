using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Entities;
using BestFor.Dto;

namespace BestFor.Models
{
    public class AdminUserViewModel
    {
        public ApplicationUser User { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}
