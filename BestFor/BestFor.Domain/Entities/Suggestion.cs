using BestFor.Domain.Interfaces;
using BestFor.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    /// <summary>
    /// Represents a simple word suggestion object
    /// </summary>
    public class Suggestion : EntityBase, IFirstIndex, IDtoConvertable<SuggestionDto>
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        public string Phrase { get; set; }

        #region IFirstIndex implementation
        [NotMapped]
        public string IndexKey { get { return Phrase.ToLower(); } }
        #endregion

        #region IDtoConvertable implementation
        public SuggestionDto ToDto()
        {
            return new SuggestionDto() { Phrase = this.Phrase };
        }

        public int FromDto(SuggestionDto dto)
        {
            Phrase = dto.Phrase;
            return Id;
        }
        #endregion
    }
}
