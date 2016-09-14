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
    public class AnswerDescriptionVoteTests
    {
        AnswerDescriptionVote _answerDescriptionVote;

        public AnswerDescriptionVoteTests()
        {
            _answerDescriptionVote = new AnswerDescriptionVote()
            {
                Id = 1,
                DateAdded = new DateTime(2016, 1, 1),
                ObjectState = ObjectState.Unchanged,
                UserId = "B",
                AnswerDescriptionId = 2
            };
        }

        [Fact]
        public void AnswerDescriptionVoteTests_Constractor_Creates()
        {
            // Verify that properties are copied in constrator
            Assert.Equal(1, _answerDescriptionVote.Id);
            // It is the same as NumberOfEntries
            Assert.Equal(new DateTime(2016, 1, 1), _answerDescriptionVote.DateAdded);
            Assert.Equal(ObjectState.Unchanged, _answerDescriptionVote.ObjectState);
            Assert.Equal("B", _answerDescriptionVote.UserId);

        }

        [Fact]
        public void AnswerDescriptionVoteTests_SetProperties_SetsAllProperties()
        {
            _answerDescriptionVote.Id++;
            // It is the same as NumberOfEntries
            _answerDescriptionVote.DateAdded = new DateTime(2016, 1, 2);

            _answerDescriptionVote.ObjectState = ObjectState.Added;
            _answerDescriptionVote.UserId = "C";
            _answerDescriptionVote.AnswerDescriptionId = 78;
            _answerDescriptionVote.NumberOfEntries = 7;

            // Verify that properties are copied in constrator
            Assert.Equal(2, _answerDescriptionVote.Id);
            // It is the same as NumberOfEntries
            Assert.Equal(78, _answerDescriptionVote.AnswerDescriptionId);
            // This is always 1
            Assert.Equal(1, _answerDescriptionVote.NumberOfEntries);

            Assert.Equal(new DateTime(2016, 1, 2), _answerDescriptionVote.DateAdded);
            Assert.Equal(ObjectState.Added, _answerDescriptionVote.ObjectState);
            Assert.Equal("C", _answerDescriptionVote.UserId);

        }

        [Fact]
        public void AnswerDescriptionVoteTests_IDtoConvertable_Converts()
        {
            var dto = _answerDescriptionVote.ToDto();

            Assert.Equal(dto.Id, _answerDescriptionVote.Id);
            Assert.Equal(dto.AnswerDescriptionId, _answerDescriptionVote.AnswerDescriptionId);
            Assert.Equal(dto.UserId, _answerDescriptionVote.UserId);
            Assert.Equal(dto.DateAdded, _answerDescriptionVote.DateAdded);

            var item = new AnswerDescriptionVote();
            item.FromDto(dto);

            Assert.Equal(item.Id, _answerDescriptionVote.Id);
            Assert.Equal(item.AnswerDescriptionId, _answerDescriptionVote.AnswerDescriptionId);
            Assert.Equal(item.UserId, _answerDescriptionVote.UserId);
            Assert.Equal(item.DateAdded, _answerDescriptionVote.DateAdded);
        }

        [Fact]
        public void AnswerDescriptionVoteTests_OtherProperties_Work()
        {
            var answerDescription = new AnswerDescription() { Description = "leftword" };

            _answerDescriptionVote.AnswerDescription = answerDescription;

            Assert.Equal(_answerDescriptionVote.AnswerDescription.Description, "leftword");

            var user = new ApplicationUser() { DisplayName = "A1" };
            _answerDescriptionVote.ApplicationUser = user;
            Assert.Equal(_answerDescriptionVote.ApplicationUser.DisplayName, "A1");
        }

        [Fact]
        public void AnswerDescriptionVoteTests_IFirstIndex_Implements()
        {
            var firstIndex = _answerDescriptionVote as IFirstIndex;

            // Verify first index is implemented correctly
            Assert.Equal(firstIndex.IndexKey, _answerDescriptionVote.AnswerDescriptionId.ToString());
            Assert.NotEqual(firstIndex.IndexKey, _answerDescriptionVote.DateAdded.ToString());
        }

        [Fact]
        public void AnswerDescriptionVoteTests_ISecondIndex_Implements()
        {
            var secondIndex = _answerDescriptionVote as ISecondIndex;

            // Verify that ISecondIndex is inplemented correctly
            Assert.Equal(secondIndex.SecondIndexKey, _answerDescriptionVote.Id.ToString());
        }
    }
}
