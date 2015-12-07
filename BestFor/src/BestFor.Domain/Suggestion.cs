using BestFor.Domain.Interfaces;
using BestFor.Dto;

namespace BestFor.Domain
{
    /// <summary>
    /// Represents a simple word suggestion object
    /// </summary>
    public class Suggestion : BaseDomainObject, IDtoConvertable<SuggestionDto>
    {
        public string Phrase { get; set; }

        public SuggestionDto ToDto()
        {
            return new SuggestionDto() { Phrase = this.Phrase };
        }

        public string FromDto(SuggestionDto dto)
        {
            Phrase = dto.Phrase;
            return Id;
        }
    }
}
