﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Dto
{
    public class HomePageDto
    {
        public AnswersDto TopToday { get; set; } = new AnswersDto();

        public AnswersDto TopOverall { get; set; } = new AnswersDto();
    }
}
