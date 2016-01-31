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

            return Id;
        }
        #endregion

    }
}
