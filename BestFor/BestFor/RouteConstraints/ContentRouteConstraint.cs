using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BestFor.RouteConstraints
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
    public class ContentRouteConstraint : BaseRouteConstraint, IRouteConstraint
    {
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
            // This should never happen according to template definition but who knows.
            if (!values.ContainsKey("content") || values["content"] == null) return false;

            string culture = GetCulture(httpContext, values);

            // For now we will only check the content although we can build a full regualr expression
            // But we will let the controller to the job.

            // Find the word "best" in the specified culture.
            // Return true if found.
            string content = values["content"].ToString().ToLower().Trim();
            string best = _allCommonStrings[culture].Best.ToLower();
            // See if url starts with /best
            return content.StartsWith(best);
        }
    }
}
