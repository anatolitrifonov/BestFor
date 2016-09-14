using BestFor.Domain.Interfaces;
using BestFor.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    /// <summary>
    /// Flag added by someone to indicate that Answer is questionable.
    /// Flag for answer and flag for answer description are separate to avoid funny database manipulations.
    /// We may end up storing cache of flags but for sure different tables.
    /// </summary>
    public class AnswerDescriptionFlag : EntityBase, IDtoConvertable<AnswerDescriptionFlagDto>
    {
        /// <summary>
        /// Identity ...
        /// </summary>
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        /// <summary>
        /// Reason for flagging
        /// </summary>
        [MaxLength(100)]
        public string Reason { get; set; }

        /// <summary>
        /// Foreign key to answer
        /// </summary>
        [Required]
        public int AnswerDescriptionId { get; set; }

        /// <summary>
        /// Parent answer object
        /// </summary>
        [ForeignKey("AnswerDescriptionId")]
        public AnswerDescription AnswerDescription { get; set; }

        /// <summary>
        /// Foreign key to user. We do not have to have it required since users can add answers without
        /// being logged in or registered.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Parent user object. User that added the answer description. Could be not the same user as the one who first added the answer.
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        #region IDtoConvertable implementation
        public AnswerDescriptionFlagDto ToDto()
        {
            return new AnswerDescriptionFlagDto()
            {
                Reason = Reason,
                UserId = UserId,
                AnswerDescriptionId = AnswerDescriptionId,
                Id = Id,
                DateAdded = DateAdded
            };
        }

        public int FromDto(AnswerDescriptionFlagDto dto)
        {
            Reason = dto.Reason;
            Id = dto.Id;
            UserId = dto.UserId;
            AnswerDescriptionId = dto.AnswerDescriptionId;
            DateAdded = dto.DateAdded;

            return Id;
        }
        #endregion
    }
}
