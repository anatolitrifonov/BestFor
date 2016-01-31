﻿using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNet.Antiforgery;

namespace BestFor.Controllers
{
    public abstract class BaseApiController : Controller
    {
        [FromServices]
        public IAntiforgery Antiforgery { get; set; }

        /// <summary>
        /// Pages working with these controllers are expected to pass the header with this name.
        /// </summary>
        public const string ANTI_FORGERY_HEADER_NAME = "ANTI_FORGERY_HEADER";

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
        /// Parses values passed in anti forgery header.
        /// The idea is that if someone blindly calls controller methods we will simply return nothing and not throw expection.
        /// Exceptions are expensive.
        /// Let's throw it only if there was something passed in the header and it was not valid.
        /// Otherwise we will just return nothing.
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
                    Antiforgery.ValidateTokens(HttpContext, new AntiforgeryTokenSet(tokens[1], tokens[0]));
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Parse the culture string from the url or return the default culture string
        /// </summary>
        /// <returns></returns>
        private string ParseCulture()
        {
            _culture = "en-US";
            var requestPath = Request.Path.ToString();
            // theoretically this can never happen
            if (string.IsNullOrEmpty(requestPath) || string.IsNullOrWhiteSpace(requestPath)) return _culture;
            // Theoretically requestPath can never start with space because spaces in URL are replaces with %20 but we will check anyway
            requestPath = requestPath.Trim();
            if (requestPath == "/") return _culture;
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
            return SetCulture("en-US");
        }

        /// <summary>
        /// Two lines shortcut to set the variable and return it.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        private string SetCulture(string culture)
        {
            _culture = culture;
            return _culture;
        }
    }
}