using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace BestFor.UnitTests.AppSettings
{
    [ExcludeFromCodeCoverage]
    public class AppSettingsUnitTests
    {
        [Fact]
        public void AppSettings_AppSettings_ReadSettings()
        {
            var settings = Common.AppSettings.ReadSettings();
            Assert.True(settings.Value.MiscSetting == "test");
        }
    }
}
