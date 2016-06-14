using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace BestFor.UnitTests.AppSettings
{
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
