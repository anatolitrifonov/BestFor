﻿using System.Globalization;
using Microsoft.AspNet.Mvc.Filters;
// using Microsoft.Framework.Logging;

namespace BestFor
{
    public class LanguageActionFilter : ActionFilterAttribute
    {
        //private readonly ILogger _logger;

        public LanguageActionFilter() // (ILoggerFactory loggerFactory)
        {
            string g = "ty";
            //_logger = loggerFactory.CreateLogger("LanguageActionFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
//            string culture = context.RouteData.Values["culture"].ToString();
//        //    _logger.LogInformation($"Setting the culture from the URL: {culture}");

//#if DNX451
//            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
//            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
//#else
//            CultureInfo.CurrentCulture = new CultureInfo(culture);
//            CultureInfo.CurrentUICulture = new CultureInfo(culture);
//#endif
            base.OnActionExecuting(context);
        }
    }
}
