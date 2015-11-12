using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestFor.Dto;
using BestFor.Domain.Interfaces;

namespace BestFor.Domain
{
    /// <summary>
    /// Represents a simple word suggestion object
    /// </summary>
    public class Suggestion: IDtoConvertable<SuggestionDto>
    {
        public string Phrase { get; set; }

        public SuggestionDto ToDto()
        {
            return new SuggestionDto() { Phrase = this.Phrase };
        }
    }
}
