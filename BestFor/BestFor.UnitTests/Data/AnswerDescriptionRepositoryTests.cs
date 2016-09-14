using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Fakes;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace BestFor.UnitTests.Data
{
    /// <summary>
    /// Tests BestFor.Data.AnswerDescriptionRepository;
    /// </summary>
    /// <remarks>Repository operations are not asynchronous. No need to make these tests asynchronous too.</remarks>
    [ExcludeFromCodeCoverage]
    public class AnswerDescriptionRepositoryTests
    {
        private FakeDataContext _dataContext;
        private AnswerDescriptionRepository _repository;
        private FakeAnswerDescriptions _fakeAnswerDescriptions;

        public AnswerDescriptionRepositoryTests()
        {
            _dataContext = new FakeDataContext();
            _fakeAnswerDescriptions = _dataContext.EntitySet<AnswerDescription>() as FakeAnswerDescriptions;

            _repository = new AnswerDescriptionRepository(_dataContext);
        }

        [Fact]
        public void AnswerDescriptionRepositoryTests_FindAnswerDescriptionsWithNoUser_OnlyDescriptionsWithoutUserReturned()
        {
            var result = _repository.FindAnswerDescriptionsWithNoUser();

            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Equal(result.Count(), 2);
        }

        [Fact]
        public void AnswerDescriptionRepositoryTests_FindByAnswerId_OnlyAnswerDescriptionsReturned()
        {
            var result = _repository.FindByAnswerId(1);

            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Equal(result.Count(), 1);
        }

        [Fact]
        public void AnswerDescriptionRepositoryTests_FindByAnswerId_OnlyUserDescriptionsReturned()
        {
            var result = _repository.FindByUserId("A");

            // FakeAnswerDescriptions have only two records with no user. 
            Assert.Equal(result.Count(), 1);
        }
    }
}
