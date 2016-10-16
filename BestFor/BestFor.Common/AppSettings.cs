using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BestFor.Common
{
    /// <summary>
    /// Represents Typed general application settings. Contains static methods to load appsettings.
    /// </summary>
    public class AppSettings
    {
        private class IOptionsImplementation : IOptions<AppSettings>
        {
            public AppSettings AppSettings { get; set; }

            public AppSettings Value { get { return AppSettings; } }
        }

        public string AmazonAccessKeyId { get; set; }

        public string AmazonAssociateId { get; set; }

        public string AmazonSecretKey { get; set; }

        public string EmailServerAddress { get; set; }

        public int EmailServerPort { get; set; }

        public string EmailServerUser { get; set; }

        public string EmailServerPassword { get; set; }

        public string EmailFromAddress { get; set; }

        public string MiscSetting { get; set; }

        public bool EnableFacebookSharing { get; set; }

        public string FullDomainAddress { get; set; }

        public string DatabaseConnectionString { get; set; }

        /// <summary>
        /// Used only for debugging to track when an instance of this class is created.
        /// </summary>
        /// <remarks>
        /// Put the break point here for debugging.
        /// </remarks>
        public AppSettings() { }

        /// <summary>
        /// This is used only for unit tests. Basically this reads the settings assuming the file in is the current folder.
        /// Usually instance is created by injection.
        /// </summary>
        /// <returns></returns>
        public static IOptions<AppSettings> ReadSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.secret.json");
            var appSettings = new AppSettings();

            builder.Build().GetSection("AppSettings").Bind(appSettings);

            return new IOptionsImplementation() { AppSettings = appSettings };
        }
    }
}
