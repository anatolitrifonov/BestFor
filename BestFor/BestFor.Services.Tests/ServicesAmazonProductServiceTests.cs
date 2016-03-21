using BestFor.Services.Cache;
using Moq;
using Xunit;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using BestFor.Services.AffiliateProgram.Amazon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using BestFor.Common;

namespace BestFor.Services.Tests
{
    public class ServicesAmazonProductServiceTests
    {
        /// <summary>
        /// Verify that we can read app confgi file
        /// </summary>
        [Fact]
        public void AmazonProductService_AppSettings_ReadSettings()
        {
            var settings = Common.AppSettings.ReadSettings();
            Assert.True(settings.Value.MiscSetting == "test");
        }

        /// <summary>
        /// Check that calling amazon service works.
        /// </summary>
        [Fact]
        public void AmazonProductService_FindProduct_FindsProduct()
        {
            var cacheMock = new Mock<ICacheManager>();
            var cache = cacheMock.Object;
            var service = new AmazonProductService(Common.AppSettings.ReadSettings(), cache);
            var product = service.FindProduct(new ProductSearchParameters() { Keyword = "fishing" });
            // Amazon may return a different product on search every time, no point in checking the exact values.
            Assert.NotNull(product);
            Assert.NotNull(product.MerchantProductId);
            Assert.NotNull(product.Title);
            Assert.NotNull(product.DetailPageURL);
        }

        private class IOptionsImplementation : IOptions<AppSettings>
        {
            public AppSettings AppSettings { get; set; }

            public AppSettings Value { get { return AppSettings; } }
        }

        [Fact]
        public void AmazonProductService_ParseReturnResult_ReturnsProduct()
        {
            // Load xml from the file
            string path = @"C:\Users\atrifono\Documents\Personal\Fork\BestFor\src\BestFor.Services\AffiliateProgram\Amazon\";
            FileStream myFileStream = new FileStream(path + "junk.xml", FileMode.Open);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(myFileStream);
            // Ask service to parse it.
            var appSettings = new IOptionsImplementation() { AppSettings = new AppSettings() };
            var cache = new Mock<ICacheManager>().Object;

            var service = new AmazonProductService(appSettings, cache);
            var product = service.ReadXml(xmlDoc);
            Assert.True(product.DetailPageURL.StartsWith("http://www.amazon.com/Total-Fishing-Manual-Field-Stream/dp/1616284870%3FSubscriptionId%3DAKIAI5A2QWLR7ECMOCWA%26tag%3Dbestfor03"));
            Assert.Equal(product.MerchantProductId, "1616284870");
            Assert.Equal(product.Title, "The Total Fishing Manual (Field & Stream): 317 Essential Fishing Skills (Field and Stream)");
        }
    }
}
