using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BestFor.RouteConstraints
{
    /// <summary>
    /// Check if URL starts with /first/{data} or /{known culture}/first/{data}
    /// The word first is actually not verified since there is no other way to get here unless url
    /// is already is correct format.
    /// Culture is also allowed to be anything.
    /// It does validate that data has something and will return false if data is blank.
    /// 
    /// The only reason we need this constraint is to check that data is not blank.
    /// </summary>
    public class SearchRouteConstraint : BaseRouteConstraint, IRouteConstraint
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
            if (!values.ContainsKey("data") || values["data"] == null) return false;

            // For now we will only check the content although we can build a full regualr expression
            // But we will let the controller to the job.

            // Find the word "best" in the specified culture.
            // Return true if found.
            string data = values["data"].ToString().ToLower().Trim();
            // See if url starts with /best
            return !string.IsNullOrEmpty(data) && !string.IsNullOrWhiteSpace(data);
        }
    }
}
