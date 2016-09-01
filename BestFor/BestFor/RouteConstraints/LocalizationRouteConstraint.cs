using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BestFor.RouteConstraints
{
    public class LocalizationRouteConstraint : IRouteConstraint
    {
        /// <summary>
        /// This contraint is used in the following mapping template: "{culture}/{controller}/{action}/{id?}",
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="route"></param>
        /// <param name="routeKey"></param>
        /// <param name="values"></param>
        /// <param name="routeDirection"></param>
        /// <returns></returns>
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            // Let's just say "yes" for now.
            // I do not see the situation when simply saying yes is not going to work ...
            // URLS that do not hit this constraint are: blank or anything starting with ?
            // Everything else hits this constraint.
            // But we do not want to complain if first part is not a culture or if second one is not a controller
            // We will let the mapping happen naturaly
            // If culture is bad, controller will serve default culture
            // If controller name is blank Home is served
            // If action name is blank Index is served
            // If controller or action are unknown then probably 404 will be served

            return true;
        }
    }
}
