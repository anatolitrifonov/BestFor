﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BestFor.Domain;
using BestFor.Dto;

namespace BestFor.Domain.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class DomainSuggestionTests
    {

        [Fact]
        public void Suggestion_ToDto()
        {
            var suggestion = new Suggestion();
            var suggestionDto = suggestion.ToDto();
            Assert.Equal(suggestion.Phrase, suggestionDto.Phrase);
        }
    }
}
