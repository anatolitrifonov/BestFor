using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Dto
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    /// <summary>
    /// Represents a simple words suggestion for typeahead text box.
    /// </summary>
    public class SuggestionDto : BaseDto
    {
        public SuggestionDto()
        {
        }
        public string Phrase { get; set; }
    }
}
