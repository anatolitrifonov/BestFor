using BestFor.Dto;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace BestFor.RouteConstraints
{
    /// <summary>
    /// Base route constraint class with some basic helper functions.
    /// Stored all common strings in all languages to the url parsing.
    /// </summary>
    public class BaseRouteConstraint
    {
        /// <summary>
        /// Store all common strings for all known cultures. 
        /// This is static and will be initialized once per application.
        /// This is done intentionally since this dictionary will be used a lot.
        /// Key     = culture code
        /// Value   = commons strings for this culture
        /// </summary>
        public static Dictionary<string, CommonStringsDto> _allCommonStrings;

        /// <summary>
        /// Scan route values to see if there is a culture in the URL or return default culture 
        /// </summary>
        /// <returns>Culture code from URL or default culture</returns>
        public string GetCulture(HttpContext httpContext, RouteValueDictionary values)
        {
            // Set common strings so that we can validate the request. Variable is static. Will happen once per application.
            if (_allCommonStrings == null)
            {
                // ResourceService is injected in start up. It loads localized strings from database and caches them.
                var service = httpContext.RequestServices.GetService(typeof(IResourcesService));
                if (service != null)
                {
                    var resourceService = (IResourcesService)service;
                    _allCommonStrings = resourceService.GetCommonStringsForAllCultures().Result;
                }
            }

            // First check if we need to be concerned about culture. Culture is there if we got here from second tempalte.
            string culture = "en-US";
            if (values.ContainsKey("country") && values.ContainsKey("language"))
            {
                // Do we know this culture?
                if (_allCommonStrings.ContainsKey(values["language"] + "-" + values["country"]))
                    culture = values["language"] + "-" + values["country"];
            }

            return culture;
        }
    }
}
