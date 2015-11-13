using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BestFor.Services;

namespace BestFor.Services.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ServicesSuggestionServiceTests
    {

        [Fact]
        public void SuggestionService_FindSuggestions_NoResult()
        {
            var suggestions = new SuggestionService();
            var result = suggestions.FindSuggestions("B");
            Assert.True(result != null);
            Assert.Equal(result.Count(), 0);
        }

        [Fact]
        public void SuggestionService_FindSuggestions_SomeResult()
        {
            var suggestions = new SuggestionService();
            var result = suggestions.FindSuggestions("A");
            Assert.True(result != null);
            Assert.Equal(result.Count(), 1);
        }
    }
}
