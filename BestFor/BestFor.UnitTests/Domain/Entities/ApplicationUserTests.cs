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
    public class ApplicationUserTests
    {
        ApplicationUser _applicationUser;

        public ApplicationUserTests()
        {
            _applicationUser = new ApplicationUser()
            {
                Id = "1sssss",
                DateAdded = new DateTime(2016, 1, 1)
            };
        }

        [Fact]
        public void ApplicationUserTests_IDtoConvertable_Converts()
        {
            var dto = _applicationUser.ToDto();

            Assert.Equal(dto.UserId, _applicationUser.Id);
            //Assert.Equal(dto.AnswerId, _answerFlag.AnswerId);
            //Assert.Equal(dto.UserId, _answerFlag.UserId);
            //Assert.Equal(dto.DateAdded, _answerFlag.DateAdded);

            var item = new ApplicationUser();
            item.FromDto(dto);

            Assert.Equal(item.Id, _applicationUser.Id);
            //Assert.Equal(item.AnswerId, _answerFlag.AnswerId);
            //Assert.Equal(item.UserId, _answerFlag.UserId);
            // This is intentional
            //Assert.NotEqual(item.DateAdded, _answerFlag.DateAdded);
        }

        //[Fact]
        //public void AnswerFlagTests_Constractor_Creates()
        //{
        //    // Verify that properties are copied in constrator
        //    Assert.Equal(1, _answerFlag.Id);
        //    // It is the same as NumberOfEntries
        //    Assert.Equal(new DateTime(2016, 1, 1), _answerFlag.DateAdded);
        //    Assert.Equal(ObjectState.Unchanged, _answerFlag.ObjectState);
        //    Assert.Equal("B", _answerFlag.UserId);

        //}

        //[Fact]
        //public void AnswerFlagTests_SetProperties_SetsAllProperties()
        //{
        //    _answerFlag.Id++;
        //    // It is the same as NumberOfEntries
        //    _answerFlag.DateAdded = new DateTime(2016, 1, 2);

        //    _answerFlag.ObjectState = ObjectState.Added;
        //    _answerFlag.UserId = "C";
        //    _answerFlag.AnswerId = 78;
        //    //_answerFlag.NumberOfEntries = 7;

        //    // Verify that properties are copied in constrator
        //    Assert.Equal(2, _answerFlag.Id);
        //    // It is the same as NumberOfEntries
        //    Assert.Equal(78, _answerFlag.AnswerId);
        //    // This is always 1
        //    //Assert.Equal(1, _answerFlag.NumberOfEntries);

        //    Assert.Equal(new DateTime(2016, 1, 2), _answerFlag.DateAdded);
        //    Assert.Equal(ObjectState.Added, _answerFlag.ObjectState);
        //    Assert.Equal("C", _answerFlag.UserId);

        //}



        //[Fact]
        //public void AnswerFlagTests_OtherProperties_Work()
        //{
        //    var answer = new Answer() { LeftWord = "leftword" };

        //    _answerFlag.Answer = answer;

        //    Assert.Equal(_answerFlag.Answer.LeftWord, "leftword");

        //    var user = new ApplicationUser() { DisplayName = "A1" };
        //    _answerFlag.ApplicationUser = user;
        //    Assert.Equal(_answerFlag.ApplicationUser.DisplayName, "A1");
        //}

        //[Fact]
        //public void AnswerFlagTests_IFirstIndex_Implements()
        //{
        //    var firstIndex = _answerFlag as IFirstIndex;

        //    // Verify first index is implemented correctly
        //    Assert.Equal(firstIndex.IndexKey, _answerFlag.AnswerId.ToString());
        //    Assert.NotEqual(firstIndex.IndexKey, _answerFlag.DateAdded.ToString());
        //}

        //[Fact]
        //public void AnswerFlagTests_ISecondIndex_Implements()
        //{
        //    var secondIndex = _answerFlag as ISecondIndex;

        //    // Verify that ISecondIndex is inplemented correctly
        //    Assert.Equal(secondIndex.SecondIndexKey, _answerFlag.Id.ToString());
        //}
    }
}
