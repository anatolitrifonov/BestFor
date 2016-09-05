using BestFor.Domain.Interfaces;
using BestFor.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    public class AnswerDescription : EntityBase, IFirstIndex, ISecondIndex, IDtoConvertable<AnswerDescriptionDto>, IIdIndex
    {
        #region IIdIndex implementation
        /// <summary>
        /// Identity ...
        /// </summary>
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        #endregion

        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Foreign key to answer
        /// </summary>
        [Required]
        public int AnswerId { get; set; }

        /// <summary>
        /// Parent answer object
        /// </summary>
        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }

        /// <summary>
        /// Foreign key to user. Checking if it has to be marked as [Required]. We do not have to have it required since users can add answers without
        /// being logged in or registered.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Parent user object. User that added the answer description. Could be not the same user as the one who first added the answer.
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        #region IFirstIndex implementation
        [NotMapped]
        public string IndexKey { get { return AnswerId.ToString(); } }
        #endregion

        #region ISecondIndex implementation
        [NotMapped]
        public string SecondIndexKey { get { return Id.ToString(); } }

        [NotMapped]
        public int NumberOfEntries { get { return 1; } set { return; } }
        #endregion

        #region IDtoConvertable implementation
        public AnswerDescriptionDto ToDto()
        {
            return new AnswerDescriptionDto()
            {
                Id = Id,
                AnswerId = AnswerId,
                Description = Description,
                UserId = UserId,
                DateAdded = DateAdded
            };
        }

        public int FromDto(AnswerDescriptionDto dto)
        {
            Id = dto.Id;
            AnswerId = dto.AnswerId;
            Description = dto.Description;
            UserId = dto.UserId;
            //DateAdded = dto.DateAdded;

            return Id;
        }
        #endregion

    }
}
