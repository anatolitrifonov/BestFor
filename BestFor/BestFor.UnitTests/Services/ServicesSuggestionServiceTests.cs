using Autofac;
using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Fakes;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using BestFor.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BestFor.UnitTests.Testables;

namespace BestFor.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class ServicesSuggestionServiceTests
    {
        /// <summary>
        /// Constructor sets up the needed data.
        /// </summary>
        private class TestSetup
        {
            public SuggestionService SuggestionService;
            public FakeSuggestions FakeSuggestions;
            public Mock<ICacheManager> CacheMock;
            public Repository<Suggestion> Repository;
            public TestLoggerFactory TestLoggerFactory;
            // public TestLogger<SuggestionService> TestLogger;

            public TestSetup()
            {
                var dataContext = new FakeDataContext();
                Repository = new Repository<Suggestion>(dataContext);
                CacheMock = new Mock<ICacheManager>();

                //LoggerMock = new Mock<ILogger<SuggestionService>>();
                //LoggerMock.Setup(x => x.LogInformation(It.IsAny<string>()));
                // LoggerMock.Setup(x => x.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()));

                // TestLogger = new TestLogger<SuggestionService>();
                TestLoggerFactory = new TestLoggerFactory();

                SuggestionService = new SuggestionService(CacheMock.Object, Repository, TestLoggerFactory);
                FakeSuggestions = dataContext.EntitySet<Suggestion>() as FakeSuggestions;
            }
        }

        [Fact]
        public async Task SuggestionService_AddSuggestions_AddsSuggestion()
        {
            // Setup
            var setup = new TestSetup();
            // Object we will be adding
            var suggestionDto = new SuggestionDto() { Phrase = "Hello" };

            // Call the method we are testing
            var result = await setup.SuggestionService.AddSuggestion(suggestionDto);

            // Check that same Phrase is returned
            Assert.Equal(result.Phrase, suggestionDto.Phrase);

            // Verify cache get was called only once
            setup.CacheMock.Verify(x => x.Get(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA), Times.Once());
            // Verify cache add to cache was called only once
            setup.CacheMock.Verify(x => x.Add(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA, It.IsAny<KeyDataSource<Suggestion>>()), Times.Once());
            // Verify repository has the item
            Assert.NotNull(setup.Repository.Queryable().Where(x => x.Phrase == suggestionDto.Phrase).FirstOrDefault());
        }

        [Fact]
        public async Task SuggestionService_FindSuggestions_SomeResults()
        {
            // Setup
            var setup = new TestSetup();

            // Call the method we are testing
            var result = await setup.SuggestionService.FindSuggestions("test");

            // Check number of test suggestions
            Assert.Equal(result.Count(), setup.FakeSuggestions.NumberOfTestSuggestions);

            // Verify that get was called only once
            setup.CacheMock.Verify(x => x.Get(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA), Times.Once());
            // Verify that Add to cache was called only once
            setup.CacheMock.Verify(x => x.Add(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA, It.IsAny<KeyDataSource<Suggestion>>()), Times.Once());

            // Call the method we are testing
            result = await setup.SuggestionService.FindSuggestions("abc");

            // Check number of abc suggestions returned is max allowed
            Assert.Equal(result.Count(), KeyDataSource<Suggestion>.DEFAULT_TOP_COUNT);

            // Verify that get was called twice
            setup.CacheMock.Verify(x => x.Get(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA), Times.Exactly(2));
            // Cache Add will also be called twice since this is a fake cache and it does not store anything.
            // We are not testing cache at the moment
            // We are testing FindSuggestions
            setup.CacheMock.Verify(x => x.Add(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA, It.IsAny<KeyDataSource<Suggestion>>()), Times.Exactly(2));
        }

        [Fact]
        public async Task SuggestionService_FindSuggestions_NoResults()
        {
            // Setup
            var setup = new TestSetup();

            // Call the method we are testing
            var result = await setup.SuggestionService.FindSuggestions("ztest");

            // Check number of test suggestions
            Assert.Equal(result.Count(), 0);

            // Verify that get was called only once
            setup.CacheMock.Verify(x => x.Get(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA), Times.Once());
            // Verify that Add to cache was called only once
            setup.CacheMock.Verify(x => x.Add(CacheConstants.CACHE_KEY_SUGGESTIONS_DATA, It.IsAny<KeyDataSource<Suggestion>>()), Times.Once());
        }
    }
}
