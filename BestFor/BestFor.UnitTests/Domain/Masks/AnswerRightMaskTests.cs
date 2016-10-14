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
    public class AnswerRightMaskTests
    {
        Answer _answer;

        public AnswerRightMaskTests()
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
        public void AnswerRightMaskTests_Constractor_Creates()
        {
            var answerRightMask = new AnswerRightMask(_answer);

            // Verify that properties are copied in constrator
            Assert.Equal(answerRightMask.Id, _answer.Id);
            Assert.Equal(answerRightMask.Count, _answer.Count);
            Assert.Equal(answerRightMask.Phrase, _answer.Phrase);
            Assert.Equal(answerRightMask.LeftWord, _answer.LeftWord);
            Assert.Equal(answerRightMask.RightWord, _answer.RightWord);
            Assert.Equal(answerRightMask.NumberOfEntries, _answer.NumberOfEntries);
        }

        [Fact]
        public void AnswerRightMaskTests_IFirstIndex_Implements()
        {
            var answerRightMask = new AnswerRightMask(_answer);

            var firstIndex = answerRightMask as IFirstIndex;

            // Verify first index is implemented correctly
            Assert.Equal(firstIndex.IndexKey, _answer.RightWord);
            Assert.NotEqual(firstIndex.IndexKey, _answer.LeftWord);
        }

        [Fact]
        public void AnswerRightMaskTests_ISecondIndex_Implements()
        {
            var answerRightMask = new AnswerRightMask(_answer);

            var secondIndex = answerRightMask as ISecondIndex;

            // Verify that properties are copied in constrator
            Assert.Equal(secondIndex.SecondIndexKey, _answer.Id.ToString());
            Assert.Equal(secondIndex.NumberOfEntries, _answer.NumberOfEntries);
        }

        [Fact]
        public void AnswerRightMaskTests_IDtoConvertable_ToDto()
        {
            var answerRightMask = new AnswerRightMask(_answer);

            var dto = answerRightMask.ToDto();

            // Verify that properties are copied in ToDto
            Assert.Equal(dto.Id, _answer.Id);
            Assert.Equal(dto.Phrase, _answer.Phrase);
            Assert.Equal(dto.LeftWord, _answer.LeftWord);
            Assert.Equal(dto.RightWord, _answer.RightWord);
            Assert.Equal(dto.Count, _answer.Count);

            // Not copied properties
            Assert.NotEqual(dto.UserId, _answer.UserId);
            Assert.NotEqual(dto.Category, _answer.Category);
            Assert.NotEqual(dto.DateAdded, _answer.DateAdded);
        }

        [Fact]
        public void AnswerRightMaskTests_IDtoConvertable_FromDto()
        {
            var answerRightMask = new AnswerRightMask(_answer);

            var dto = _answer.ToDto();

            // reload from dto
            answerRightMask.FromDto(dto);


            // Verify that properties are copied in ToDto
            Assert.Equal(dto.Id, answerRightMask.Id);
            Assert.Equal(dto.Phrase, answerRightMask.Phrase);
            Assert.Equal(dto.LeftWord, answerRightMask.LeftWord);
            Assert.Equal(dto.RightWord, answerRightMask.RightWord);
            Assert.Equal(dto.Count, answerRightMask.Count);

            answerRightMask.NumberOfEntries = _answer.NumberOfEntries;

            Assert.Equal(answerRightMask.NumberOfEntries, _answer.NumberOfEntries);

        }
    }
}
