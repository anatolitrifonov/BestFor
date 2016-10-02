using System;
using System.Threading;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Antiforgery;

namespace BestFor.Controllers
{
    /// <summary>
    /// Contins AntiForgery protection and culture parsing functions helping inheriting api controllers.
    /// 
    /// ResponseCache applies to all controllers if I am not mistaken.
    /// </summary>
    [ResponseCache(CacheProfileName = "Hello")]
    public abstract class BaseApiController : Controller
    {
        //[FromServices]
        public IAntiforgery Antiforgery { get; set; }

        /// <summary>
        /// Pages working with these controllers are expected to pass the header with this name.
        /// </summary>
        public const string ANTI_FORGERY_HEADER_NAME = "ANTI_FORGERY_HEADER";
        /// <summary>
        /// Sometimes pages will send this cookie.
        /// </summary>
        public const string ANTI_FORGERY_COOKIE_NAME = "ANTI_FORGERY_COOKIE_NAME";
        public const string DEBUG_REACTJS_URL_PARAMETER_NAME = "debugreact";

        public const string DEFAULT_CULTURE = "en-US";

        /// <summary>
        /// URL may contain referene to cuture as /culture/controller/action/something
        /// This class will parse the culture and store it between calls to save on parsing.
        /// Unfortunately url is parsed twice: one time in routing contraint and another one here if controller asks for culture value.
        /// </summary>
        private string _culture = null;
        public string Culture
        {
            get
            {
                
                return _culture ?? ParseCulture();
            }
        }

        /// <summary>
        /// Cut the culture in request path
        /// </summary>
        protected string RequestPathNoCulture
        {
            get
            {
                var requestPath = Request.Path.Value;
                // cut the culture
                var cultureBegining = "/" + Culture.ToLower();
                if (requestPath.ToLower().StartsWith(cultureBegining)) requestPath = requestPath.Substring(cultureBegining.Length);
                return requestPath;
            }
        }

        /// <summary>
        /// Parses values passed in anti forgery header.
        /// The idea is that if someone blindly calls controller methods we will simply return nothing and not throw expection.
        /// Exceptions are expensive.
        /// Let's throw it only if there was something passed in the header and it was not valid.
        /// Otherwise we will just return nothing.
        /// 
        /// Look at the help folder for more explanation.
        /// 
        /// Used by inheriting controller to protect the straight calls to controllers.
        /// Also see file is Help folder about controllers protection
        /// </summary>
        /// <returns></returns>
        protected bool ParseAntiForgeryHeader()
        {
            StringValues tokenHeaders;
            if (HttpContext.Request.Headers.TryGetValue(ANTI_FORGERY_HEADER_NAME, out tokenHeaders))
            {
                var tokens = tokenHeaders.First().Split(':');
                if (tokens != null && tokens.Length == 2)
                {
                    var cookieToken = tokens[0];
                    var formToken = tokens[1];
                    if (string.IsNullOrEmpty(cookieToken) || string.IsNullOrWhiteSpace(cookieToken))
                    {
                        // Try the cookies
                        if (Request.Cookies.Keys.Contains(ANTI_FORGERY_COOKIE_NAME))
                        {
                            cookieToken = Request.Cookies[ANTI_FORGERY_COOKIE_NAME];
                        }
                    }

                    //antiforgery.ValidateRequestAsync(HttpContext);

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Read Url parameter as string
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        protected string ReadUrlParameter(string parameterName)
        {
            if (!Request.Query.ContainsKey(parameterName)) return null;
            return Request.Query[parameterName][0];
        }

        /// <summary>
        /// Read Url parameter as bool
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        protected bool ReadUrlParameterAsBoolean(string parameterName)
        {
            bool result;
            if (bool.TryParse(ReadUrlParameter(parameterName), out result)) return result;
            return false;
        }

        #region Private Methods
        /// <summary>
        /// Parse the culture string from the url or return the default culture string
        /// </summary>
        /// <returns></returns>
        private string ParseCulture()
        {
            _culture = DEFAULT_CULTURE;
            var requestPath = Request.Path.ToString();
            // theoretically this can never happen
            if (string.IsNullOrEmpty(requestPath) || string.IsNullOrWhiteSpace(requestPath)) return SetCulture(_culture);
            // Theoretically requestPath can never start with space because spaces in URL are replaces with %20 but we will check anyway
            requestPath = requestPath.Trim();
            if (requestPath == "/") return SetCulture(_culture);
            // URL "/////blah" is converted by browser to "/blah". Therefor we can never get into the situation of path being "///blah".
            // Although I only checked Chrome
            // first remove the starting "/"
            if (requestPath.StartsWith("/")) requestPath = requestPath.Substring(1);
            // We do not need split since it is expensive. We only need the first word.
            // Reminder that it can't be // or blank so condition is 
            var culture = requestPath.IndexOf("/") > 0 ? requestPath.Substring(0, requestPath.IndexOf("/")).ToLower() : requestPath.ToLower();
            // We can also try to trim the culture but this would be rather extreme.
            // Got the culture ... now let's see if we know it.
            if (culture == "fi" || culture == "fi-fi") return SetCulture("fi-FI");
            if (culture == "ru" || culture == "ru-ru") return SetCulture("ru-RU");
            return SetCulture(DEFAULT_CULTURE);
        }

        /// <summary>
        /// Two lines shortcut to set the variable and return it.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        private string SetCulture(string culture)
        {
            _culture = culture;
            // Save the culture in the thread.
            Thread.SetData(Thread.GetNamedDataSlot("SavedThreadCulture"), culture);
            return _culture;
        }
        #endregion
    }
}
