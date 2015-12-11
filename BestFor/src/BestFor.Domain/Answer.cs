using System;
using BestFor.Domain.Interfaces;
using BestFor.Dto;

namespace BestFor.Domain
{
    public class Answer : BaseDomainObject, IDtoConvertable<AnswerDto>
    {
        public string LeftWord { get; set; }

        public string RightWord { get; set; }

        public string Phrase { get; set; }

        public int Count { get; set; }

        public string Key { get { return Answer.FormKey(LeftWord , RightWord); } }

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

        public ObjectsIdentifier FromDto(AnswerDto dto)
        {
            Phrase = dto.Phrase;
            LeftWord = dto.LeftWord;
            RightWord = dto.RightWord;
            Count = dto.Count;

            return Id;
        }
    }
}
