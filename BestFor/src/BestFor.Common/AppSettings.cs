using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BestFor.Common
{
    /// <summary>
    /// Represents general application settings. Contains static methods to load appsettings.
    /// </summary>
    public class AppSettings
    {
        public string AmazonAccessKeyId { get; set; }
        public string AmazonAssociateId { get; set; }
        public string AmazonSecretKey { get; set; }

        public string MiscSetting { get; set; }

        public static AppSettings ReadSettings()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var appSettings = new AppSettings();
            var config = builder.Build();
            ConfigurationBinder.Bind(config.GetSection("AppSettings"), appSettings);
            return appSettings;
        }
    }
}
