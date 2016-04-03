using BestFor.Services.Messaging;
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
    public class ServicesEmailTests
    {
        /// <summary>
        /// Verify that we can read app confgi file
        /// </summary>
        [Fact]
        public async Task EmailSender_SendTestEmail_SendsEmail()
        {
            var settings = AppSettings.ReadSettings();
            var sender = new AuthMessageSender(settings);
            await sender.SendEmailAsync("anatoli@trifonov.com", "Hello email test", "Hells kitchen");
            Assert.True(settings.Value.MiscSetting == "test");
        }
    }
}
