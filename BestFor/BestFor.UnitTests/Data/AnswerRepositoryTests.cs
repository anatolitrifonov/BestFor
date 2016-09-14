using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Fakes;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace BestFor.UnitTests.Data
{
    /// <summary>
    /// Tests BestFor.Data.AnswerRepository
    /// </summary>
    /// <remarks>Repository operations are not asynchronous. No need to make these tests asynchronous too.</remarks>
    [ExcludeFromCodeCoverage]
    public class AnswerRepositoryTests
    {
        private FakeDataContext _dataContext;
        private AnswerRepository _repository;
        private FakeAnswers _fakeAnswers;

        public AnswerRepositoryTests()
        {
            _dataContext = new FakeDataContext();
            _fakeAnswers = _dataContext.EntitySet<Answer>() as FakeAnswers;

            _repository = new AnswerRepository(_dataContext);
        }

        [Fact]
        public void AnswerRepositoryTests_FindAnswersTrendingToday_OnlyTrendingTodayReturned()
        {
            var result = _repository.FindAnswersTrendingToday(2, new DateTime(2016, 1, 2));

            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Equal(result.Count(), 2);
        }

        [Fact]
        public void AnswerRepositoryTests_FindAnswersTrendingToday_ExceptionOnInvalidParameter()
        {
            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _repository.FindAnswersTrendingToday(-1, new DateTime(2016, 1, 2));
                }
            );

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _repository.FindAnswersTrendingToday(1002, new DateTime(2016, 1, 2));
                }
            );
        }

        [Fact]
        public void AnswerRepositoryTests_FindAnswersTrendingOverall_OnlyTrendingOverallReturned()
        {
            var result = _repository.FindAnswersTrendingOverall(2);

            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Equal(result.Count(), 2);
        }

        [Fact]
        public void AnswerRepositoryTests_FindAnswersTrendingOverall_ExceptionOnInvalidParameter()
        {
            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _repository.FindAnswersTrendingOverall(-1);
                }
            );

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _repository.FindAnswersTrendingOverall(1002);
                }
            );
        }

        [Fact]
        public void AnswerRepositoryTests_Active_OnlyActiveReturned()
        {
            var result = _repository.Active();

            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Equal(result.Count(), 6);
        }

        [Fact]
        public void AnswerRepositoryTests_FindByUserId_OnlyUserAnswersReturned()
        {
            var result = _repository.FindByUserId("A");

            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Equal(result.Count(), 3);
        }

        [Fact]
        public void AnswerRepositoryTests_FindAnswersWithNoUser_OnlyAnswersWithNoUserReturned()
        {
            var result = _repository.FindAnswersWithNoUser();

            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Equal(result.Count(), 6);
        }
    }
}
