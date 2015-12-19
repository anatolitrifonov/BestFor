using System;
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
    }
}
