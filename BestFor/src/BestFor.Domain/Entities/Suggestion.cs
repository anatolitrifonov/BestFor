using System.ComponentModel.DataAnnotations;
using BestFor.Domain.Interfaces;
using BestFor.Dto;

namespace BestFor.Domain.Entities
{
    /// <summary>
    /// Represents a simple word suggestion object
    /// </summary>
    public class Suggestion : IDtoConvertable<SuggestionDto>
    {
        [Key]
        public int Id { get; set; }

        public string Phrase { get; set; }

        public SuggestionDto ToDto()
        {
            return new SuggestionDto() { Phrase = this.Phrase };
        }

        public int FromDto(SuggestionDto dto)
        {
            Phrase = dto.Phrase;
            return Id;
        }
    }
}
