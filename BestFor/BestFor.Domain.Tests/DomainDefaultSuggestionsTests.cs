using BestFor.Domain;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BestFor.Domain.Tests
{
    /// <summary>
    /// Unit tests for DefaultSuggestions object
    /// </summary>
    public class DomainDefaultSuggestionsTests
    {
        [Fact]
        public void DefaultSuggestions_FindSuggestions_NoResult()
        {
            var suggestions = new DefaultSuggestions();
            var result = suggestions.FindSuggestions("B");
            Assert.True(result != null);
            Assert.Equal(result.Count(), 0);
        }

        [Fact]
        public void DefaultSuggestions_FindSuggestions_SomeResult()
        {
            var suggestions = new DefaultSuggestions();
            var result = suggestions.FindSuggestions("A");
            Assert.True(result != null);
            Assert.Equal(result.Count(), 1);
        }
    }
}
