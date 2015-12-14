using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestFor.Domain.Interfaces;
using BestFor.Dto;

namespace BestFor.Domain.Entities
{
    public class Answer : IndexableEntity, IDtoConvertable<AnswerDto>
    {
        [Key]
        public override int Id { get; set; }

        public string LeftWord { get; set; }

        public string RightWord { get; set; }

        public string Phrase { get; set; }

        public int Count { get; set; }

        [NotMapped]
        public override string IndexKey { get { return Answer.FormKey(LeftWord , RightWord); } }

        public static string FormKey(string leftWord, string rightWord) { return leftWord + " " + rightWord; }

        public AnswerDto ToDto()
        {
            return new AnswerDto()
            {
                Phrase = Phrase,
                LeftWord = LeftWord,
                RightWord = RightWord,
                Count = Count
            };
        }

        public int FromDto(AnswerDto dto)
        {
            Phrase = dto.Phrase;
            LeftWord = dto.LeftWord;
            RightWord = dto.RightWord;
            Count = dto.Count;

            return Id;
        }
    }
}
