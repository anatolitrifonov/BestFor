using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestFor.Dto;

namespace BestFor.Domain.Entities
{
    public class AnswerDescription : EntityBase, IDtoConvertable<AnswerDescriptionDto>
    {
        [Required]
        public string Description { get; set; }

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
            Description = dto.Description;

            return Id;
        }
        #endregion

    }
}
