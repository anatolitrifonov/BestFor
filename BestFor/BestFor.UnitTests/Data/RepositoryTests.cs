using BestFor.Data;
using BestFor.Domain;
using BestFor.Domain.Entities;
using BestFor.Fakes;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Xunit;

namespace BestFor.UnitTests.Data
{
    /// <summary>
    /// Tests BestFor.Data.Repository
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RepositoryTests
    {
        private FakeDataContext _dataContext;
        private Repository<Answer> _repository;

        public RepositoryTests()
        {
            _dataContext = new FakeDataContext();
            _repository = new Repository<Answer>(_dataContext);
        }

        [Fact]
        public void RepositoryTests_GetById_ReturnsNull()
        {
            Assert.Null(_repository.GetById(1));
        }

        [Fact]
        public void RepositoryTests_List_ReturnsDataset()
        {
            var fake = new FakeAnswers();
            Assert.Equal(_repository.List().Count(), fake.Count());
        }

        [Fact]
        public void RepositoryTests_Active_ReturnsActive()
        {
            var fake = new FakeAnswers();
            Assert.Equal(_repository.Active().Count(), fake.Count());
        }

        [Fact]
        public void RepositoryTests_Queryable_ReturnsQueryable()
        {
            var fake = new FakeAnswers();
            Assert.Equal(_repository.Queryable().Count(), fake.Count());
        }

        [Fact]
        public void RepositoryTests_Count_ReturnsCount()
        {
            var fake = new FakeAnswers();
            Assert.Equal(_repository.Count(), fake.Count());
        }

        [Fact]
        public void RepositoryTests_Insert_Inserts()
        {
            var answer = new Answer();
            _repository.Insert(answer);
            Assert.Equal(answer.ObjectState, ObjectState.Added);

            // Check that one item was added
            var fake = new FakeAnswers();
            Assert.Equal(_repository.Count(), fake.Count() + 1);
        }

        [Fact]
        public void RepositoryTests_Update_Updates()
        {
            var answer = new Answer() ;
            _repository.Update(answer);
            Assert.Equal(answer.ObjectState, ObjectState.Modified);

            // Check that count stayed the same
            var fake = new FakeAnswers();
            Assert.Equal(_repository.Count(), fake.Count());
        }

        [Fact]
        public async void RepositoryTests_SaveChangesAsync_SavesChanges()
        {
            var dataContextMock = new Mock<FakeDataContext>();
            // dataContextMock.Setup(x => x.A());

            var repository = new Repository<Answer>(dataContextMock.Object);

            CancellationToken token = new CancellationToken();

            await repository.SaveChangesAsync(token);

            // verify that SaveChangesAsync on repo calls SaveChangesAsync on dataContext
            dataContextMock.Verify(x => x.SaveChangesAsync(token), Times.Once());
        }
    }
}
