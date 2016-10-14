using BestFor.Domain;
using BestFor.Domain.Entities;
using BestFor.Domain.Interfaces;
using BestFor.Domain.Masks;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace BestFor.UnitTests.Domain.Masks
{
    [ExcludeFromCodeCoverage]
    public class AnswerUserMaskTests
    {
        Answer _answer;

        public AnswerUserMaskTests()
        {
            _answer = new Answer()
            {
                Id = 1,
                Count = 2,
                Category = "A",
                DateAdded = DateTime.Now.AddDays(1),
                IsHidden = false,
                LeftWord = "left",
                RightWord = "right",
                NumberOfEntries = 3,
                ObjectState = ObjectState.Unchanged,
                Phrase = "phrase",
                UserId = "B"
            };
        }

        [Fact]
        public void AnswerUserMaskTests_Constractor_Creates()
        {
            var answerUserMask = new AnswerUserMask(_answer);

            // Verify that properties are copied in constrator
            Assert.Equal(answerUserMask.Id, _answer.Id);
            Assert.Equal(answerUserMask.Count, _answer.Count);
            Assert.Equal(answerUserMask.Phrase, _answer.Phrase);
            Assert.Equal(answerUserMask.LeftWord, _answer.LeftWord);
            Assert.Equal(answerUserMask.RightWord, _answer.RightWord);
            Assert.Equal(answerUserMask.NumberOfEntries, _answer.NumberOfEntries);
        }

        [Fact]
        public void AnswerUserMaskTests_IFirstIndex_Implements()
        {
            var answerUserMask = new AnswerUserMask(_answer);

            var firstIndex = answerUserMask as IFirstIndex;

            // Verify first index is implemented correctly
            Assert.Equal(firstIndex.IndexKey, _answer.UserId);
            Assert.NotEqual(firstIndex.IndexKey, _answer.LeftWord);
        }

        [Fact]
        public void AnswerUserMaskTests_ISecondIndex_Implements()
        {
            var answerUserMask = new AnswerUserMask(_answer);

            var secondIndex = answerUserMask as ISecondIndex;

            // Verify that properties are copied in constrator
            Assert.Equal(secondIndex.SecondIndexKey, _answer.Id.ToString());
            Assert.Equal(secondIndex.NumberOfEntries, _answer.NumberOfEntries);
        }

        [Fact]
        public void AnswerUserMaskTests_IDtoConvertable_ToDto()
        {
            var answerUserMask = new AnswerUserMask(_answer);

            var dto = answerUserMask.ToDto();

            // Verify that properties are copied in ToDto
            Assert.Equal(dto.Id, _answer.Id);
            Assert.Equal(dto.Phrase, _answer.Phrase);
            Assert.Equal(dto.LeftWord, _answer.LeftWord);
            Assert.Equal(dto.RightWord, _answer.RightWord);
            Assert.Equal(dto.Count, _answer.Count);
            Assert.Equal(dto.UserId, _answer.UserId);

            // Not copied properties
            Assert.NotEqual(dto.Category, _answer.Category);
            Assert.NotEqual(dto.DateAdded, _answer.DateAdded);
        }

        [Fact]
        public void AnswerUserMaskTests_IDtoConvertable_FromDto()
        {
            var answerUserMask = new AnswerUserMask(_answer);

            var dto = _answer.ToDto();

            // reload from dto
            answerUserMask.FromDto(dto);


            // Verify that properties are copied in ToDto
            Assert.Equal(dto.Id, answerUserMask.Id);
            Assert.Equal(dto.Phrase, answerUserMask.Phrase);
            Assert.Equal(dto.LeftWord, answerUserMask.LeftWord);
            Assert.Equal(dto.RightWord, answerUserMask.RightWord);
            Assert.Equal(dto.Count, answerUserMask.Count);

            answerUserMask.NumberOfEntries = _answer.NumberOfEntries;

            Assert.Equal(answerUserMask.NumberOfEntries, _answer.NumberOfEntries);

        }
    }
}
