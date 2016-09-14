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
    public class AnswerLeftMaskTests
    {
        Answer _answer;

        public AnswerLeftMaskTests()
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
        public void AnswerLeftMaskTests_Constractor_Creates()
        {
            var answerLeftMask = new AnswerLeftMask(_answer);

            // Verify that properties are copied in constrator
            Assert.Equal(answerLeftMask.Id, _answer.Id);
            Assert.Equal(answerLeftMask.Count, _answer.Count);
            Assert.Equal(answerLeftMask.Phrase, _answer.Phrase);
            Assert.Equal(answerLeftMask.LeftWord, _answer.LeftWord);
            Assert.Equal(answerLeftMask.RightWord, _answer.RightWord);
            Assert.Equal(answerLeftMask.NumberOfEntries, _answer.NumberOfEntries);
        }

        [Fact]
        public void AnswerLeftMaskTests_IFirstIndex_Implements()
        {
            var answerLeftMask = new AnswerLeftMask(_answer);

            var firstIndex = answerLeftMask as IFirstIndex;

            // Verify first index is implemented correctly
            Assert.Equal(firstIndex.IndexKey, _answer.LeftWord);
            Assert.NotEqual(firstIndex.IndexKey, _answer.RightWord);
        }

        [Fact]
        public void AnswerLeftMaskTests_ISecondIndex_Implements()
        {
            var answerLeftMask = new AnswerLeftMask(_answer);

            var secondIndex = answerLeftMask as ISecondIndex;

            // Verify that properties are copied in constrator
            Assert.Equal(secondIndex.SecondIndexKey, _answer.Id.ToString());
            Assert.Equal(secondIndex.NumberOfEntries, _answer.NumberOfEntries);
        }

        [Fact]
        public void AnswerLeftMaskTests_IDtoConvertable_ToDto()
        {
            var answerLeftMask = new AnswerLeftMask(_answer);

            var dto = answerLeftMask.ToDto();

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
        public void AnswerLeftMaskTests_IDtoConvertable_FromDto()
        {
            var answerLeftMask = new AnswerLeftMask(_answer);

            var dto = _answer.ToDto();

            // reload from dto
            answerLeftMask.FromDto(dto);


            // Verify that properties are copied in ToDto
            Assert.Equal(dto.Id, answerLeftMask.Id);
            Assert.Equal(dto.Phrase, answerLeftMask.Phrase);
            Assert.Equal(dto.LeftWord, answerLeftMask.LeftWord);
            Assert.Equal(dto.RightWord, answerLeftMask.RightWord);
            Assert.Equal(dto.Count, answerLeftMask.Count);

            answerLeftMask.NumberOfEntries = _answer.NumberOfEntries;

            Assert.Equal(answerLeftMask.NumberOfEntries, _answer.NumberOfEntries);

        }
    }
}
