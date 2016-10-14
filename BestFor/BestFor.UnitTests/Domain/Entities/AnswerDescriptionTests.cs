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
    public class AnswerDescriptionTests
    {
        AnswerDescription _answerDescription;

        public AnswerDescriptionTests()
        {
            _answerDescription = new AnswerDescription()
            {
                Id = 1,
                DateAdded = new DateTime(2016, 1, 1),
                NumberOfEntries = 3,
                ObjectState = ObjectState.Unchanged,
                UserId = "B",
                AnswerId = 2,
                Description = "A"
            };
        }

        [Fact]
        public void AnswerDescriptionTests_Constractor_Creates()
        {
            // Verify that properties are copied in constrator
            Assert.Equal(1, _answerDescription.Id);
            // It is the same as NumberOfEntries
            Assert.Equal("A", _answerDescription.Description);
            // _answerDescription.NumberOfEntries is always 1 no matter what.
            Assert.Equal(1, _answerDescription.NumberOfEntries);
            Assert.Equal(2, _answerDescription.AnswerId);

            Assert.Equal(new DateTime(2016, 1, 1), _answerDescription.DateAdded);
            Assert.Equal(ObjectState.Unchanged, _answerDescription.ObjectState);
            Assert.Equal("B", _answerDescription.UserId);

        }

        [Fact]
        public void AnswerDescriptionTests_SetProperties_SetsAllProperties()
        {
            _answerDescription.Id++;
            // It is the same as NumberOfEntries
            _answerDescription.Description = "phrase1";
            _answerDescription.DateAdded = new DateTime(2016, 1, 2);

            _answerDescription.ObjectState = ObjectState.Added;
            _answerDescription.UserId = "C";
            _answerDescription.AnswerId = 78;

            // Verify that properties are copied in constrator
            Assert.Equal(2, _answerDescription.Id);
            // It is the same as NumberOfEntries
            Assert.Equal(78, _answerDescription.AnswerId);
            Assert.Equal("phrase1", _answerDescription.Description);
            // _answerDescription.NumberOfEntries is always 1 no matter what.
            Assert.Equal(1, _answerDescription.NumberOfEntries);

            Assert.Equal(new DateTime(2016, 1, 2), _answerDescription.DateAdded);
            Assert.Equal(ObjectState.Added, _answerDescription.ObjectState);
            Assert.Equal("C", _answerDescription.UserId);

        }

        [Fact]
        public void AnswerDescriptionTests_IDtoConvertable_Converts()
        {
            var dto = _answerDescription.ToDto();

            Assert.Equal(dto.Id, _answerDescription.Id);
            Assert.Equal(dto.AnswerId, _answerDescription.AnswerId);
            Assert.Equal(dto.Description, _answerDescription.Description);
            Assert.Equal(dto.UserId, _answerDescription.UserId);
            Assert.Equal(dto.DateAdded, _answerDescription.DateAdded);

            var newAnswerDescription = new AnswerDescription();
            newAnswerDescription.FromDto(dto);

            Assert.Equal(newAnswerDescription.Id, _answerDescription.Id);
            Assert.Equal(newAnswerDescription.Description, _answerDescription.Description);
            Assert.Equal(newAnswerDescription.AnswerId, _answerDescription.AnswerId);
            Assert.Equal(newAnswerDescription.UserId, _answerDescription.UserId);
            Assert.NotEqual(newAnswerDescription.DateAdded, _answerDescription.DateAdded);
        }

        [Fact]
        public void AnswerDescriptionTests_OtherProperties_Work()
        {
            var answer = new Answer() { LeftWord = "leftword" };

            _answerDescription.Answer = answer;

            Assert.Equal(_answerDescription.Answer.LeftWord, "leftword");

            var user = new ApplicationUser() { DisplayName = "A" };
            _answerDescription.ApplicationUser = user;
            Assert.Equal(_answerDescription.ApplicationUser.DisplayName, "A");
        }

        [Fact]
        public void AnswerDescriptionTests_IFirstIndex_Implements()
        {
            var firstIndex = _answerDescription as IFirstIndex;

            // Verify first index is implemented correctly
            Assert.Equal(firstIndex.IndexKey, _answerDescription.AnswerId.ToString());
            Assert.NotEqual(firstIndex.IndexKey, _answerDescription.Description);
        }

        [Fact]
        public void AnswerDescriptionTests_ISecondIndex_Implements()
        {
            var secondIndex = _answerDescription as ISecondIndex;

            // Verify that ISecondIndex is inplemented correctly
            Assert.Equal(secondIndex.SecondIndexKey, _answerDescription.Id.ToString());
        }
    }
}
