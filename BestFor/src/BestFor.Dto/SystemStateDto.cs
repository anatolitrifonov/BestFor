using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Dto
{
    public class SystemStateDto
    {
        public string AnswersCacheStatus { get; set; }

        public int AnswersCacheNumberItems { get; set; }

        public string SuggestionsCacheStatus { get; set; }

        public int SuggestionsCacheNumberItems { get; set; }

        public string BadWordsCacheStatus { get; set; }

        public int BadWordsCacheNumberItems { get; set; }
    }
}
