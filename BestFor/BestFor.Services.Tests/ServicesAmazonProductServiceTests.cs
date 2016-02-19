using Xunit;
using System.Threading.Tasks;
using BestFor.Services.AffiliateProgram.Amazon;

namespace BestFor.Services.Tests
{
    public class ServicesAmazonProductServiceTests
    {
        /// <summary>
        /// Verify that we can read app confgi file
        /// </summary>
        [Fact]
        public void AmazonProductService_FindProduct_FindsProduct()
        {
            var settings = Common.AppSettings.ReadSettings();
            Assert.True(settings.MiscSetting == "test");

            var service = new AmazonProductService(settings.AmazonAccessKeyId, settings.AmazonSecretKey, settings.AmazonAssociateId);
            service.FindProduct("fishing");
        }

        [Fact]
        public void AmazonProductService_Junk()
        {
            var settings = Common.AppSettings.ReadSettings();
            Assert.True(settings.MiscSetting == "test");

            var service = new AmazonProductService(settings.AmazonAccessKeyId, settings.AmazonSecretKey, settings.AmazonAssociateId);
            service.ReadXml();
        }
    }
}
