using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BestFor.Services.Services;
using BestFor.Dto;

namespace BestFor
{
    /// <summary>
    /// Matches if URL starts with "best"
    /// There are two ways we can get here
    /// 1. template: "{content}"
    /// 2. template: "{language}-{country}/{content}"
    /// Check if we support localization for language and country if not or blank default to English US
    /// Check if content is in format "<best in language><something><for in language><something><is in language><something>"
    /// Return match if it is
    /// </summary>
    public class ContentRouteConstraint : IRouteConstraint
    {
        /// <summary>
        /// Store all common strings for all known cultures. 
        /// This is static and will be initialized once per application.
        /// This is done intentionally since this dictionary wll be used a lot.
        /// </summary>
        private static Dictionary<string, CommonStringsDto> _allCommonStrings;

        //
        // Summary:
        //     Determines whether the URL parameter contains a valid value for this constraint.
        //
        // Parameters:
        //   httpContext:
        //     An object that encapsulates information about the HTTP request.
        //
        //   route:
        //     The router that this constraint belongs to.
        //
        //   routeKey:
        //     The name of the parameter that is being checked.
        //
        //   values:
        //     A dictionary that contains the parameters for the URL.
        //
        //   routeDirection:
        //     An object that indicates whether the constraint check is being performed when
        //     an incoming request is being handled or when a URL is being generated.
        //
        // Returns:
        //     true if the URL parameter contains a valid value; otherwise, false.

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, 
            RouteDirection routeDirection)
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

            // This should never happen according to template definition but who knows.
            if (!values.ContainsKey("content") || values["content"] == null) return false;

            // First check if we need to be concerned about culture. Culture is there if we got here from second tempalte.
            string culture = "en-US";
            if (values.ContainsKey("country") && values.ContainsKey("language"))
            {
                // Do we know this culture?
                if (_allCommonStrings.ContainsKey(values["language"] + "-" + values["country"]))
                    culture = values["language"] + "-" + values["country"];
            }

            // For now we will only check the content although we can build a full regualr expression
            // But we will let the controller to the job.

            // Find the word "best" in the specified culture.
            // Return true if found.
            return values["content"].ToString().ToLower().Trim().StartsWith(_allCommonStrings[culture].Best.ToLower());
        }
    }
}
