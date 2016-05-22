﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestFor.Dto;

namespace BestFor.Domain.Entities
{
    /// <summary>
    /// Flag added by someone to indicate that Answer is questionable.
    /// Flag for answer and flag for answer description are separate to avoid funny database manipulations.
    /// We may end up storing cache of flags but for sure different tables.
    /// </summary>
    public class AnswerDescriptionVote : EntityBase, IFirstIndex, ISecondIndex, IDtoConvertable<AnswerDescriptionVoteDto>
    {
        /// <summary>
        /// Identity ...
        /// </summary>
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

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
        
        #region IFirstIndex implementation
        [NotMapped]
        public string IndexKey { get { return AnswerDescriptionId.ToString(); } }
        #endregion

        #region ISecondIndex implementation
        [NotMapped]
        public string SecondIndexKey { get { return Id.ToString(); } }

        [NotMapped]
        public int NumberOfEntries { get { return 1; } set { return; } }
        #endregion

        #region IDtoConvertable implementation
        public AnswerDescriptionVoteDto ToDto()
        {
            return new AnswerDescriptionVoteDto()
            {
                UserId = UserId,
                AnswerDescriptionId = AnswerDescriptionId,
                Id = Id
            };
        }

        public int FromDto(AnswerDescriptionVoteDto dto)
        {
            Id = dto.Id;
            UserId = dto.UserId;
            AnswerDescriptionId = dto.AnswerDescriptionId;

            return Id;
        }
        #endregion
    }
}
