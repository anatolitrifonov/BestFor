using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Domain.Entities;
using BestFor.Dto;

namespace BestFor.Models
{
    public class AdminAnswerViewModel
    {
        public AnswerDto Answer { get; set; }

        public IEnumerable<AnswerDescriptionDto> AnswerDescriptions { get; set; }
    }
}
