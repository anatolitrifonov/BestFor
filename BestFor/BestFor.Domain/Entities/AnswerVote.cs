using BestFor.Domain.Interfaces;
using BestFor.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    /// <summary>
    /// Vote added by someone to indicate they favor.
    /// Vote for answer and vote for answer description are separate to avoid funny database manipulations.
    /// We may end up storing cache of votes but for sure different tables.
    /// </summary>
    public class AnswerVote : EntityBase, IFirstIndex, ISecondIndex, IDtoConvertable<AnswerVoteDto>, IIdIndex
    {
        #region IIdIndex implementation
        /// <summary>
        /// Identity ...
        /// </summary>
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        #endregion

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
        public string IndexKey { get { return AnswerId.ToString(); } }
        #endregion

        #region ISecondIndex implementation
        [NotMapped]
        public string SecondIndexKey { get { return Id.ToString(); } }

        [NotMapped]
        public int NumberOfEntries { get { return 1; } set { return; } }
        #endregion

        #region IDtoConvertable implementation
        public AnswerVoteDto ToDto()
        {
            return new AnswerVoteDto()
            {
                AnswerId = AnswerId,
                UserId = UserId,
                Id = Id,
                DateAdded = DateAdded
            };
        }

        public int FromDto(AnswerVoteDto dto)
        {
            Id = dto.Id;
            UserId = dto.UserId;
            AnswerId = dto.AnswerId;
            // Intentionall do not do that because UI does not set date.
            // We never update votes.
            // Whenever new object is created and inserted we set the creation date on object instantiation
            // Once created and saved this value is only relevant for reading.
            // DateAdded = dto.DateAdded;

            return Id;
        }
        #endregion
    }
}
