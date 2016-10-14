using BestFor.Domain.Interfaces;
using BestFor.Dto;
using BestFor.Domain.Entities;

namespace BestFor.Domain.Masks
{
    /// <summary>
    /// Mask answer by user id - index by user id.
    /// 
    /// This class masks answer object giving a different implementation 
    /// of IFirstIndex, ISecondIndex, IDtoConvertable<AnswerDto> interfaces.
    /// 
    /// This is done to be able to store different answer indexes.
    /// 
    /// Not stored in the database because this is an effective copy of the answer.
    /// 
    /// If any of the properties is of type reference we will be screwed in memory leaks.
    /// </summary>
    public class AnswerUserMask : IFirstIndex, ISecondIndex, IDtoConvertable<AnswerDto>, IIdIndex
    {
        /// <summary>
        /// Copy data from answer
        /// </summary>
        /// <param name="answer"></param>
        public AnswerUserMask(Answer answer)
        {
            Id = answer.Id;
            LeftWord = answer.LeftWord;
            RightWord = answer.RightWord;
            Phrase = answer.Phrase;
            Count = answer.Count;
            UserId = answer.UserId;
        }

        #region IIdIndex implementaion
        public int Id { get; set; }
        #endregion

        public string LeftWord { get; set; }

        public string RightWord { get; set; }

        public string Phrase { get; set; }

        public int Count { get; set; }

        public string UserId { get; set; }

        #region IFirstIndex implementation
        /// <summary>
        /// This is an alternative implementation of index key
        /// </summary>
        public string IndexKey { get { return UserId; } }
        #endregion

        #region ISecondIndex implementation
        public string SecondIndexKey { get { return Id.ToString(); } }

        public int NumberOfEntries { get { return Count; } set { Count = value; } }
        #endregion

        #region IDtoConvertable implementation
        public AnswerDto ToDto()
        {
            return new AnswerDto()
            {
                Phrase = Phrase,
                LeftWord = LeftWord,
                RightWord = RightWord,
                Count = Count,
                Id = Id,
                UserId = UserId //,
                //Category = Category
            };
        }

        public int FromDto(AnswerDto dto)
        {
            Phrase = dto.Phrase;
            LeftWord = dto.LeftWord;
            RightWord = dto.RightWord;
            Count = dto.Count;
            Id = dto.Id;
            UserId = dto.UserId;
            //Category = dto.Category;

            return Id;
        }
        #endregion

    }
}
