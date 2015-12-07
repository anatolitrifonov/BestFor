using System;
using BestFor.Domain.Interfaces;
using BestFor.Dto;

namespace BestFor.Domain
{
    public class Answer : IDtoConvertable<AnswerDto>
    {
        public Guid Id { get; set; }

        public string LeftWord { get; set; }

        public string RightWord { get; set; }

        public string Phrase { get; set; }

        public AnswerDto ToDto()
        {
            return new AnswerDto()
            {
                Phrase = Phrase,
                LeftWord = LeftWord,
                RightWord = RightWord
            };
        }

        public string FromDto(AnswerDto dto)
        {
            Phrase = dto.Phrase;
            LeftWord = dto.LeftWord;
            RightWord = dto.RightWord;

            return Id;
        }
    }
}
