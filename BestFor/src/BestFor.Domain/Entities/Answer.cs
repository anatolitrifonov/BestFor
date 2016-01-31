using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestFor.Dto;
using System.Collections.Generic;

namespace BestFor.Domain.Entities
{
    public class Answer : EntityBase, IFirstIndex, ISecondIndex, IDtoConvertable<AnswerDto>
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        public string LeftWord { get; set; }

        [Required]
        public string RightWord { get; set; }

        [Required]
        public string Phrase { get; set; }

        public int Count { get; set; }

        public static string FormKey(string leftWord, string rightWord) { return leftWord + " " + rightWord; }

        // Navigation property 
        public virtual ICollection<AnswerDescription> AnswerDescriptions { get; set; }


        #region IFirstIndex implementation
        [NotMapped]
        public string IndexKey { get { return Answer.FormKey(LeftWord , RightWord); } }
        #endregion

        #region ISecondIndex implementation
        [NotMapped]
        public string SecondIndexKey { get { return Phrase; } }

        [NotMapped]
        public int NumberOfEntries { get { return Count; } set { Count = value; } }
        #endregion

        #region IDtoConvertable implementation
        public AnswerDto ToDto()
        {
            return new AnswerDto()
            {
                Phrase = Phrase,
                LeftWord = LeftWord,
                RightWord = RightWord,
                Count = Count,
                Id = Id
            };
        }

        public int FromDto(AnswerDto dto)
        {
            Phrase = dto.Phrase;
            LeftWord = dto.LeftWord;
            RightWord = dto.RightWord;
            Count = dto.Count;
            Id = dto.Id;

            return Id;
        }
        #endregion
    }
}
