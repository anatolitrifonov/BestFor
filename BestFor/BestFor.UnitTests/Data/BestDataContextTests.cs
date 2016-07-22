using BestFor.Data;
using System.Linq;
using Xunit;

namespace BestFor.UnitTests.Data
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BestDataContextTests
    {

        [Fact]
        public void BestDataContext_OnConfiguring_CanReadConfigFile()
        {
            var dataContext = new BestDataContext();

            bool any = dataContext.BadWords.Any();

            // Simply check if there are any bad words in the database.
            // Useless test but we want to prove something while debugging
            Assert.True(any);
        }
    }
}
