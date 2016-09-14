using Autofac;
using BestFor.Data;
using BestFor.Domain;
using BestFor.Domain.Entities;
using BestFor.Services.DataSources;
using System.Threading.Tasks;
using Xunit;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.UnitTests.Services
{
    /// <summary>
    /// Unit tests for DefaultSuggestions object
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ServicesKeyIndexedDataSourceTests : BaseTest
    {
        //[Fact]
        //public async Task KeyIndexedDataSource_FakeInitialize_Initializes()
        //{
        //    var indexDataSource = new KeyIndexedDataSource<Answer>();

        //    var repo = new Repository<Answer>(resolver.Resolve<IDataContext>());

        //    await indexDataSource.Initialize(repo.Active());
        //    //var suggestions = new DefaultSuggestions();
        //    //var result = suggestions.FindSuggestions("B");
        //    //Assert.True(result != null);
        //    //Assert.Equal(result.Count(), 0);
        //}

        //[Fact]
        //public void KeyIndexedDataSource_LiveInitialize_Initializes()
        //{
        //    var indexDataSource = new KeyIndexedDataSource<Answer>();

        //    var dataContext = new BestDataContext();

        //    // var repo = new Repository<Answer>(fakeDataContext);
        //    var repo = new Repository<Answer>(dataContext);
        //    indexDataSource.Initialize(repo);
        //    //var suggestions = new DefaultSuggestions();
        //    //var result = suggestions.FindSuggestions("B");
        //    //Assert.True(result != null);
        //    //Assert.Equal(result.Count(), 0);
        //}
    }
}
