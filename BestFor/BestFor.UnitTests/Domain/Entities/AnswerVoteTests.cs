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
    public class AnswerVoteTests
    {
        AnswerVote _answerVote;

        public AnswerVoteTests()
        {
            _answerVote = new AnswerVote()
            {
                Id = 1,
                DateAdded = new DateTime(2016, 1, 1),
                ObjectState = ObjectState.Unchanged,
                UserId = "B",
                AnswerId = 2
            };
        }

        [Fact]
        public void AnswerVoteTests_Constractor_Creates()
        {
            // Verify that properties are copied in constrator
            Assert.Equal(1, _answerVote.Id);
            // It is the same as NumberOfEntries
            Assert.Equal(new DateTime(2016, 1, 1), _answerVote.DateAdded);
            Assert.Equal(ObjectState.Unchanged, _answerVote.ObjectState);
            Assert.Equal("B", _answerVote.UserId);

        }

        [Fact]
        public void AnswerVoteTests_SetProperties_SetsAllProperties()
        {
            _answerVote.Id++;
            // It is the same as NumberOfEntries
            _answerVote.DateAdded = new DateTime(2016, 1, 2);

            _answerVote.ObjectState = ObjectState.Added;
            _answerVote.UserId = "C";
            _answerVote.AnswerId = 78;
            _answerVote.NumberOfEntries = 7;

            // Verify that properties are copied in constrator
            Assert.Equal(2, _answerVote.Id);
            // It is the same as NumberOfEntries
            Assert.Equal(78, _answerVote.AnswerId);
            // This is always 1
            Assert.Equal(1, _answerVote.NumberOfEntries);

            Assert.Equal(new DateTime(2016, 1, 2), _answerVote.DateAdded);
            Assert.Equal(ObjectState.Added, _answerVote.ObjectState);
            Assert.Equal("C", _answerVote.UserId);

        }

        [Fact]
        public void AnswerVoteTests_IDtoConvertable_Converts()
        {
            var dto = _answerVote.ToDto();

            Assert.Equal(dto.Id, _answerVote.Id);
            Assert.Equal(dto.AnswerId, _answerVote.AnswerId);
            Assert.Equal(dto.UserId, _answerVote.UserId);
            Assert.Equal(dto.DateAdded, _answerVote.DateAdded);

            var item = new AnswerVote();
            item.FromDto(dto);

            Assert.Equal(item.Id, _answerVote.Id);
            Assert.Equal(item.AnswerId, _answerVote.AnswerId);
            Assert.Equal(item.UserId, _answerVote.UserId);
            // This is intentional
            Assert.NotEqual(item.DateAdded, _answerVote.DateAdded);
        }

        [Fact]
        public void AnswerVoteTests_OtherProperties_Work()
        {
            var answer = new Answer() { LeftWord = "leftword" };

            _answerVote.Answer = answer;

            Assert.Equal(_answerVote.Answer.LeftWord, "leftword");

            var user = new ApplicationUser() { DisplayName = "A1" };
            _answerVote.ApplicationUser = user;
            Assert.Equal(_answerVote.ApplicationUser.DisplayName, "A1");
        }

        [Fact]
        public void AnswerVoteTests_IFirstIndex_Implements()
        {
            var firstIndex = _answerVote as IFirstIndex;

            // Verify first index is implemented correctly
            Assert.Equal(firstIndex.IndexKey, _answerVote.AnswerId.ToString());
            Assert.NotEqual(firstIndex.IndexKey, _answerVote.DateAdded.ToString());
        }

        [Fact]
        public void AnswerVoteTests_ISecondIndex_Implements()
        {
            var secondIndex = _answerVote as ISecondIndex;

            // Verify that ISecondIndex is inplemented correctly
            Assert.Equal(secondIndex.SecondIndexKey, _answerVote.Id.ToString());
        }
    }
}
