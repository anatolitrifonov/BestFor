using log4net;
using log4net.Config;
using Microsoft.Dnx.Runtime;
//using Microsoft.Framework.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.AspNet.Hosting;

using BestFor.Services.Logging;

namespace BestFor.Extensions
{
    public static class Log4NetAspExtensions
    {
        public static void ConfigureLog4Net(this IApplicationEnvironment appEnv, string configFileRelativePath)
        {
            GlobalContext.Properties["appRoot"] = appEnv.ApplicationBasePath;
            XmlConfigurator.Configure(new FileInfo(Path.Combine(appEnv.ApplicationBasePath, configFileRelativePath)));
        }

        public static void AddLog4Net(this ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new Log4NetProvider());
        }
    }
}
