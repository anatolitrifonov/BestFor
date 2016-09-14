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
    public class AnswerDescriptionFlagTests
    {
        AnswerDescriptionFlag _answerDescriptionFlag;

        public AnswerDescriptionFlagTests()
        {
            _answerDescriptionFlag = new AnswerDescriptionFlag()
            {
                Id = 1,
                DateAdded = new DateTime(2016, 1, 1),
                ObjectState = ObjectState.Unchanged,
                UserId = "B",
                Reason = "C",
                AnswerDescriptionId = 2
            };
        }

        [Fact]
        public void AnswerDescriptionFlagTests_Constractor_Creates()
        {
            // Verify that properties are copied in constrator
            Assert.Equal(1, _answerDescriptionFlag.Id);
            // It is the same as NumberOfEntries
            Assert.Equal("C", _answerDescriptionFlag.Reason);
            Assert.Equal(new DateTime(2016, 1, 1), _answerDescriptionFlag.DateAdded);
            Assert.Equal(ObjectState.Unchanged, _answerDescriptionFlag.ObjectState);
            Assert.Equal("B", _answerDescriptionFlag.UserId);

        }

        [Fact]
        public void AnswerDescriptionFlagTests_SetProperties_SetsAllProperties()
        {
            _answerDescriptionFlag.Id++;
            // It is the same as NumberOfEntries
            _answerDescriptionFlag.Reason = "phrase1";
            _answerDescriptionFlag.DateAdded = new DateTime(2016, 1, 2);

            _answerDescriptionFlag.ObjectState = ObjectState.Added;
            _answerDescriptionFlag.UserId = "C";
            _answerDescriptionFlag.AnswerDescriptionId = 78;

            // Verify that properties are copied in constrator
            Assert.Equal(2, _answerDescriptionFlag.Id);
            // It is the same as NumberOfEntries
            Assert.Equal(78, _answerDescriptionFlag.AnswerDescriptionId);
            Assert.Equal("phrase1", _answerDescriptionFlag.Reason);

            Assert.Equal(new DateTime(2016, 1, 2), _answerDescriptionFlag.DateAdded);
            Assert.Equal(ObjectState.Added, _answerDescriptionFlag.ObjectState);
            Assert.Equal("C", _answerDescriptionFlag.UserId);

        }

        [Fact]
        public void AnswerDescriptionFlagTests_IDtoConvertable_Converts()
        {
            var dto = _answerDescriptionFlag.ToDto();

            Assert.Equal(dto.Id, _answerDescriptionFlag.Id);
            Assert.Equal(dto.AnswerDescriptionId, _answerDescriptionFlag.AnswerDescriptionId);
            Assert.Equal(dto.Reason, _answerDescriptionFlag.Reason);
            Assert.Equal(dto.UserId, _answerDescriptionFlag.UserId);
            Assert.Equal(dto.DateAdded, _answerDescriptionFlag.DateAdded);

            var newAnswerDescriptionFlag = new AnswerDescriptionFlag();
            newAnswerDescriptionFlag.FromDto(dto);

            Assert.Equal(newAnswerDescriptionFlag.Id, _answerDescriptionFlag.Id);
            Assert.Equal(newAnswerDescriptionFlag.Reason, _answerDescriptionFlag.Reason);
            Assert.Equal(newAnswerDescriptionFlag.AnswerDescriptionId, _answerDescriptionFlag.AnswerDescriptionId);
            Assert.Equal(newAnswerDescriptionFlag.UserId, _answerDescriptionFlag.UserId);
            Assert.Equal(newAnswerDescriptionFlag.DateAdded, _answerDescriptionFlag.DateAdded);
        }

        [Fact]
        public void AnswerDescriptionFlagTests_OtherProperties_Work()
        {
            var answerDescription = new AnswerDescription() { Description = "leftword" };

            _answerDescriptionFlag.AnswerDescription = answerDescription;

            Assert.Equal(_answerDescriptionFlag.AnswerDescription.Description, "leftword");

            var user = new ApplicationUser() { DisplayName = "A1" };
            _answerDescriptionFlag.ApplicationUser = user;
            Assert.Equal(_answerDescriptionFlag.ApplicationUser.DisplayName, "A1");
        }
    }
}
