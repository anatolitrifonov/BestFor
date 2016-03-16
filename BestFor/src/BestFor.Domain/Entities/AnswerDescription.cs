using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestFor.Dto;

namespace BestFor.Domain.Entities
{
    public class AnswerDescription : EntityBase, IDtoConvertable<AnswerDescriptionDto>
    {
        /// <summary>
        /// Identity ...
        /// </summary>
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

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

        #region IDtoConvertable implementation
        public AnswerDescriptionDto ToDto()
        {
            return new AnswerDescriptionDto()
            {
                Description = Description
            };
        }

        public int FromDto(AnswerDescriptionDto dto)
        {
            Id = dto.Id;
            AnswerId = dto.AnswerId;
            Description = dto.Description;
            UserId = dto.UserId;

            return Id;
        }
        #endregion

    }
}
