using BestFor.Domain;
using BestFor.Dto.Account;
using BestFor.Domain.Entities;
using BestFor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace BestFor.UnitTests.Dto
{
    [ExcludeFromCodeCoverage]
    public class ApplicationUserDtoTests
    {
        ApplicationUserDto _applicationUserDto;

        public ApplicationUserDtoTests()
        {
            _applicationUserDto = new ApplicationUserDto()
            {
                Id = 1,
                DateAdded = new DateTime(2016, 1, 1),
                UserName = "A",
                DisplayName = "B"
            };
        }

        [Fact]
        public void ApplicationUserDtoTests_DisplayNameProperty_ReturnDisplyName()
        {
            Assert.Equal("B", _applicationUserDto.DisplayName);
            _applicationUserDto.DisplayName = null;
            Assert.Equal("A", _applicationUserDto.DisplayName);
        }
    }
}
