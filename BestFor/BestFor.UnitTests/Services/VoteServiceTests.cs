using Autofac;
using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Fakes;
using BestFor.Services;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using BestFor.Services.Services;
using BestFor.UnitTests.Testables;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace BestFor.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class VoteServiceTests
    {
        /// <summary>
        /// Constructor sets up the needed data.
        /// </summary>
        private class TestSetup
        {
            public VoteService VoteService;
            public Mock<ICacheManager> CacheMock;
            public Mock<IAnswerDescriptionService> AnswerDescriptionServiceMock;
            public Repository<AnswerVote> AnswerVotesRepository;
            public Repository<AnswerDescriptionVote> AnswerDescriptionVoteRepository;
            public TestLoggerFactory TestLoggerFactory;

            public TestSetup()
            {
                var dataContext = new FakeDataContext();
                AnswerVotesRepository = new Repository<AnswerVote>(dataContext);
                AnswerDescriptionVoteRepository = new Repository<AnswerDescriptionVote>(dataContext);
                CacheMock = new TestCacheManager().CacheMock;

                AnswerDescriptionServiceMock = new Mock<IAnswerDescriptionService>();
                // Suppose we always adding answer 15
                AnswerDescriptionServiceMock.Setup(
                    x => x.FindByAnswerDescriptionId(It.IsAny<int>())
                    ).Returns(Task.FromResult(new AnswerDescriptionDto() { AnswerId = 15 }));
                TestLoggerFactory = new TestLoggerFactory();

                VoteService = new VoteService(
                    AnswerDescriptionServiceMock.Object,
                    CacheMock.Object,
                    AnswerVotesRepository,
                    AnswerDescriptionVoteRepository,
                    TestLoggerFactory);
            }
        }

        [Fact]
        public void VoteService_VoteAnswerNullData_ThrowsException()
        {
            // Setup
            var setup = new TestSetup();

            // Call the method we are testing
            // (input parameters) => expression // Expression Lambdas

            Assert.ThrowsAny<ServicesException>(() => setup.VoteService.VoteAnswer(null));

            Assert.ThrowsAny<ServicesException>(() => setup.VoteService.VoteAnswer(new AnswerVoteDto() { AnswerId = 0 }));

            // This will throw exception because there is no UserId
            Assert.ThrowsAny<ServicesException>(() => setup.VoteService.VoteAnswer(new AnswerVoteDto() { AnswerId = 1 }));
        }

        [Fact]
        public void VoteService_VoteExistingAnswer_DoesNotAddAgain()
        {
            // Setup
            var setup = new TestSetup();

            var answerVote1 = new AnswerVoteDto() { AnswerId = 1, UserId = "1" };
            var answerVote2 = new AnswerVoteDto() { AnswerId = 1, UserId = "1" };

            var count = setup.AnswerVotesRepository.Queryable().Where(x => x.UserId == "1").Count();
            Assert.Equal(0, count);
            setup.VoteService.VoteAnswer(answerVote1);
            count = setup.AnswerVotesRepository.Queryable().Where(x => x.UserId == "1").Count();
            Assert.Equal(1, count);
            setup.VoteService.VoteAnswer(answerVote2);
            // Verify insert was called only once.
            count = setup.AnswerVotesRepository.Queryable().Where(x => x.UserId == "1").Count();
            Assert.Equal(1, count);
        }

        [Fact]
        public void VoteService_VoteAnswer_AddsVote()
        {
            // Setup
            var setup = new TestSetup();
            // Object we will be adding
            var answerVoteDto = new AnswerVoteDto() { AnswerId = 5, UserId ="D" };

            // Call the method we are testing
            var result = setup.VoteService.VoteAnswer(answerVoteDto);

            // Check that same Phrase is returned
            Assert.Equal(result, answerVoteDto.AnswerId);

            // Verify cache get was called only once
            setup.CacheMock.Verify(x => x.Get(CacheConstants.CACHE_KEY_VOTES_DATA), Times.Once());
            // Verify cache add to cache was called only once
            setup.CacheMock.Verify(x => x.Add(CacheConstants.CACHE_KEY_VOTES_DATA,
                It.IsAny<KeyIndexedDataSource<AnswerVote>>()), Times.Once());
            // Verify repository has the item
            Assert.NotNull(setup.AnswerVotesRepository.Queryable()
                .Where(x => x.AnswerId == answerVoteDto.AnswerId).FirstOrDefault());
        }

        [Fact]
        public void VoteService_VoteAnswerDescriptionNullData_ThrowsException()
        {
            // Setup
            var setup = new TestSetup();

            // Call the method we are testing
            // (input parameters) => expression // Expression Lambdas

            Assert.ThrowsAny<ServicesException>(() => setup.VoteService.VoteAnswerDescription(null));

            Assert.ThrowsAny<ServicesException>(() => setup
                .VoteService.VoteAnswerDescription(new AnswerDescriptionVoteDto() { AnswerDescriptionId = 0 }));

            // This will throw exception because there is no UserId
            Assert.ThrowsAny<ServicesException>(() => setup
                .VoteService.VoteAnswerDescription(new AnswerDescriptionVoteDto() { AnswerDescriptionId = 1 }));
        }

        [Fact]
        public void VoteService_VoteExistingAnswerDescription_DoesNotAddAgain()
        {
            // Setup
            var setup = new TestSetup();

            var answerDescriptionVote1 = new AnswerDescriptionVoteDto() { AnswerDescriptionId = 1, UserId = "1" };
            var answerDescriptionVote2 = new AnswerDescriptionVoteDto() { AnswerDescriptionId = 1, UserId = "1" };

            var count = setup.AnswerDescriptionVoteRepository.Queryable().Where(x => x.UserId == "1").Count();
            Assert.Equal(0, count);
            setup.VoteService.VoteAnswerDescription(answerDescriptionVote1);
            count = setup.AnswerDescriptionVoteRepository.Queryable().Where(x => x.UserId == "1").Count();
            Assert.Equal(1, count);
            setup.VoteService.VoteAnswerDescription(answerDescriptionVote2);
            // Verify insert was called only once.
            count = setup.AnswerDescriptionVoteRepository.Queryable().Where(x => x.UserId == "1").Count();
            Assert.Equal(1, count);
        }

        [Fact]
        public void VoteService_VoteAnswerDescription_AddsDescription()
        {
            // Setup
            var setup = new TestSetup();
            // Object we will be adding
            var answerDescriptionVoteDto = new AnswerDescriptionVoteDto() { AnswerDescriptionId = 55, UserId = "D" };

            // Call the method we are testing
            var result = setup.VoteService.VoteAnswerDescription(answerDescriptionVoteDto);

            // Check that same Phrase is returned
            Assert.Equal(result, 15);

            // Verify cache get was called only once
            setup.CacheMock.Verify(x => x.Get(CacheConstants.CACHE_KEY_DESCRIPTION_VOTES_DATA), Times.Once());
            // Verify cache add to cache was called only once
            setup.CacheMock.Verify(x => x.Add(CacheConstants.CACHE_KEY_DESCRIPTION_VOTES_DATA,
                It.IsAny<KeyIndexedDataSource<AnswerDescriptionVote>>()), Times.Once());
            // Verify repository has the item
            Assert.NotNull(setup.AnswerDescriptionVoteRepository.Queryable()
                .Where(x => x.AnswerDescriptionId == answerDescriptionVoteDto.AnswerDescriptionId).FirstOrDefault());
        }

        [Fact]
        public void VoteService_VoteExistingAnswerDescription_DoesNotCacheAgain()
        {
            // Setup
            var setup = new TestSetup();

            var answerDescriptionVote1 = new AnswerDescriptionVoteDto() { AnswerDescriptionId = 1, UserId = "1" };
            var answerDescriptionVote2 = new AnswerDescriptionVoteDto() { AnswerDescriptionId = 2, UserId = "1" };

            setup.VoteService.VoteAnswerDescription(answerDescriptionVote1);
            setup.VoteService.VoteAnswerDescription(answerDescriptionVote2);

            // Verify cache add to cache was called only once
            setup.CacheMock.Verify(x => x.Add(CacheConstants.CACHE_KEY_DESCRIPTION_VOTES_DATA,
                It.IsAny<KeyIndexedDataSource<AnswerDescriptionVote>>()), Times.Once());
        }

        [Fact]
        public void VoteService_VoteExistingAnswer_DoesNotCacheAgain()
        {
            // Setup
            var setup = new TestSetup();

            var answerVote1 = new AnswerVoteDto() { AnswerId = 1, UserId = "1" };
            var answerVote2 = new AnswerVoteDto() { AnswerId = 2, UserId = "1" };

            setup.VoteService.VoteAnswer(answerVote1);
            setup.VoteService.VoteAnswer(answerVote2);

            // Verify cache add to cache was called only once
            setup.CacheMock.Verify(x => x.Add(CacheConstants.CACHE_KEY_VOTES_DATA,
                It.IsAny<KeyIndexedDataSource<AnswerVote>>()), Times.Once());
        }

        [Fact]
        public void VoteService_CountAnswerVotes_ReturnsAnswerVotes()
        {
            // Setup
            var setup = new TestSetup();

            var answerVote1 = new AnswerVoteDto() { Id = 1234, AnswerId = 111, UserId = "1" };
            var answerVote2 = new AnswerVoteDto() { Id = 1235, AnswerId = 111, UserId = "2" };

            setup.VoteService.VoteAnswer(answerVote1);
            setup.VoteService.VoteAnswer(answerVote2);

            var count = setup.VoteService.CountAnswerVotes(111);
            // Verify that cache has 2 votes.
            Assert.Equal(2, count);

            count = setup.VoteService.CountAnswerVotes(0);
            // Verify that cache has 2 votes.
            Assert.Equal(0, count);

            count = setup.VoteService.CountAnswerVotes(112);
            // Verify that cache does not contain random items.
            Assert.Equal(0, count);

        }
    }
}
