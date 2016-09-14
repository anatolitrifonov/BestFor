using BestFor.Domain;
using BestFor.Domain.Entities;
using BestFor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace BestFor.UnitTests.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class AnswerTests
    {
        Answer _answer;

        public AnswerTests()
        {
            _answer = new Answer()
            {
                Id = 1,
                Count = 2,
                Category = "A",
                DateAdded = new DateTime(2016, 1, 1),
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
        public void AnswerTests_Constractor_Creates()
        {
            // Verify that properties are copied in constrator
            Assert.Equal(1, _answer.Id);
            // It is the same as NumberOfEntries
            Assert.Equal(3, _answer.Count);
            Assert.Equal("phrase", _answer.Phrase);
            Assert.Equal("left", _answer.LeftWord);
            Assert.Equal("right", _answer.RightWord);
            Assert.Equal(3, _answer.NumberOfEntries);

            Assert.Equal("A", _answer.Category);
            Assert.Equal(new DateTime(2016, 1, 1), _answer.DateAdded);
            Assert.Equal(false, _answer.IsHidden);
            Assert.Equal(ObjectState.Unchanged, _answer.ObjectState);
            Assert.Equal("B", _answer.UserId);

        }

        [Fact]
        public void AnswerTests_SetProperties_SetsAllProperties()
        {
            _answer.Id++;
            // It is the same as NumberOfEntries
            _answer.Count++;
            _answer.Phrase = "phrase1";
            _answer.LeftWord = "left1";
            _answer.RightWord = "right1";

            _answer.Category = "A1";
            _answer.DateAdded = new DateTime(2016, 1, 2);
            _answer.IsHidden = true;

            _answer.ObjectState = ObjectState.Added;
            _answer.UserId = "C";

            // Verify that properties are copied in constrator
            Assert.Equal(2, _answer.Id);
            // It is the same as NumberOfEntries
            Assert.Equal(4, _answer.Count);
            Assert.Equal("phrase1", _answer.Phrase);
            Assert.Equal("left1", _answer.LeftWord);
            Assert.Equal("right1", _answer.RightWord);
            Assert.Equal(4, _answer.NumberOfEntries);

            Assert.Equal("A1", _answer.Category);
            Assert.Equal(new DateTime(2016, 1, 2), _answer.DateAdded);
            Assert.Equal(true, _answer.IsHidden);
            Assert.Equal(ObjectState.Added, _answer.ObjectState);
            Assert.Equal("C", _answer.UserId);

        }


        [Fact]
        public void AnswerTests_FormKey_ReturnsKey()
        {
            // Verify that properties are copied in constrator
            Assert.Equal(Answer.FormKey(_answer.LeftWord, _answer.RightWord), _answer.LeftWord + " " + _answer.RightWord);
        }

        [Fact]
        public void AnswerTests_IDtoConvertable_Converts()
        {
            var dto = _answer.ToDto();

            Assert.Equal(dto.Id, _answer.Id);
            Assert.Equal(dto.Phrase, _answer.Phrase);
            Assert.Equal(dto.LeftWord, _answer.LeftWord);
            Assert.Equal(dto.RightWord, _answer.RightWord);
            Assert.Equal(dto.Count, _answer.Count);
            Assert.Equal(dto.UserId, _answer.UserId);
            Assert.Equal(dto.Category, _answer.Category);
            Assert.Equal(dto.DateAdded, _answer.DateAdded);

            var newAnswer = new Answer();
            newAnswer.FromDto(dto);

            Assert.Equal(newAnswer.Id, _answer.Id);
            Assert.Equal(newAnswer.Phrase, _answer.Phrase);
            Assert.Equal(newAnswer.LeftWord, _answer.LeftWord);
            Assert.Equal(newAnswer.RightWord, _answer.RightWord);
            Assert.Equal(newAnswer.Count, _answer.Count);
            Assert.Equal(newAnswer.UserId, _answer.UserId);
            Assert.Equal(newAnswer.Category, _answer.Category);
            Assert.NotEqual(newAnswer.DateAdded, _answer.DateAdded);
        }

        [Fact]
        public void AnswerTests_OtherProperties_Work()
        {
            var descriptions = new List<AnswerDescription>();
            descriptions.Add(new AnswerDescription() { Description = "A" });
            

            _answer.AnswerDescriptions = descriptions;

            Assert.Equal(_answer.AnswerDescriptions.First().Description, "A");
            Assert.Equal(_answer.AnswerDescriptions.Count(), 1);

            var user = new ApplicationUser() { DisplayName = "A" };
            _answer.ApplicationUser = user;
            Assert.Equal(_answer.ApplicationUser.DisplayName, "A");
        }

        [Fact]
        public void AnswerTests_IFirstIndex_Implements()
        {
            var firstIndex = _answer as IFirstIndex;

            // Verify first index is implemented correctly
            Assert.Equal(firstIndex.IndexKey, Answer.FormKey(_answer.LeftWord, _answer.RightWord));
            Assert.NotEqual(firstIndex.IndexKey, _answer.LeftWord);
        }

        [Fact]
        public void AnswerTests_ISecondIndex_Implements()
        {
            var secondIndex = _answer as ISecondIndex;

            // Verify that ISecondIndex is inplemented correctly
            Assert.Equal(secondIndex.SecondIndexKey, _answer.Phrase);
        }
    }
}
