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
            Assert.NotNull(settings.Value.AmazonAccessKeyId);
            Assert.Equal(settings.Value.AmazonAssociateId, "c");
            Assert.NotNull(settings.Value.AmazonSecretKey);
            Assert.NotNull(settings.Value.EmailServerAddress);
            Assert.Equal(settings.Value.EmailServerPort, 50);
            Assert.NotNull(settings.Value.EmailServerUser);
            Assert.NotNull(settings.Value.EmailServerPassword);
            Assert.NotNull(settings.Value.EmailFromAddress);
            Assert.True(settings.Value.EnableFacebookSharing);
            Assert.NotNull(settings.Value.FullDomainAddress);
            Assert.Equal(settings.Value.DatabaseConnectionString, "a");
            settings.Value.FullDomainAddress = "a";
            Assert.Equal(settings.Value.FullDomainAddress, "a");
        }
    }
}
