using BestFor.Domain.Interfaces;
using BestFor.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    public class Answer : EntityBase, IFirstIndex, ISecondIndex, IDtoConvertable<AnswerDto>, IIdIndex
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

        public bool IsHidden { get; set; }

        /// <summary>
        /// Usually = searchindex name at least for amazon
        /// </summary>
        public string Category { get; set; }

        public static string FormKey(string leftWord, string rightWord) { return leftWord.ToLower() + " " + rightWord.ToLower(); }

        /// <summary>
        /// Foreign key to user. Checking if it has to be marked as [Required]. We do not have to have it required since users can add answers without
        /// being logged in or registered.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Parent user object. User that added the answer.
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        // Navigation property to children objects
        public virtual ICollection<AnswerDescription> AnswerDescriptions { get; set; }


        #region IFirstIndex implementation
        [NotMapped]
        public string IndexKey { get { return Answer.FormKey(LeftWord , RightWord); } }
        #endregion

        #region ISecondIndex implementation
        [NotMapped]
        public string SecondIndexKey { get { return Phrase.ToLower(); } }

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
                Id = Id,
                UserId = UserId,
                Category = Category,
                DateAdded = DateAdded
            };
        }

        public int FromDto(AnswerDto dto)
        {
            Phrase = dto.Phrase;
            LeftWord = dto.LeftWord;
            RightWord = dto.RightWord;
            Count = dto.Count;
            Id = dto.Id;
            UserId = dto.UserId;
            Category = dto.Category;
            //DateAdded = dto.DateAdded;

            return Id;
        }
        #endregion
    }
}
