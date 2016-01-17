using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace BestFor
{
    public class LocalizationRouteConstraint : IRouteConstraint
    {
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
