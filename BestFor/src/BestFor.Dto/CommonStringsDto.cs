using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Dto
{
    /// <summary>
    /// Used by resource service to pass around commongly known strings
    /// </summary>
    public class CommonStringsDto
    {
        public string Best { get; set; } = "Best";

        public string For { get; set; } = "for";

        public string Is { get; set; } = "is";

        public string FlagLower { get; set; } = "flag";

        public string FlagUpper { get; set; } = "Flag";
    }
}
