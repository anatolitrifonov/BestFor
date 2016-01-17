using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace BestFor
{
    /// <summary>
    /// Matches if URL starts with "best"
    /// </summary>
    public class ContentRouteConstraint : IRouteConstraint
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
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, IDictionary<string, object> values, 
            RouteDirection routeDirection)
        {
            if (values.ContainsKey("mylink") && values.ContainsKey("controller") && values.ContainsKey("action"))
            {
                if (values["controller"] != null && values["action"] != null && values["mylink"] != null)
                {
                    if (values["controller"].ToString() == "Home" && values["action"].ToString() == "MyContent" &&
                        !string.IsNullOrEmpty(values["mylink"].ToString()) && !string.IsNullOrWhiteSpace(values["mylink"].ToString()))
                    {
                        return values["mylink"].ToString().ToLower().Trim().StartsWith("best");
                    }
                }
            }
                
            return false;
        }
    }
}
