using Microsoft.AspNetCore.Http;

namespace BestFor.Resources
{
    /// <summary>
    /// This class stores the reference to the IHttpContextAccessor object injected in application start up.
    /// </summary>
    public static class BestHttpHelper
    {
        static BestHttpHelper()
        {
            // checking that this is called once per application life
            // var t = "45";
        }

        /// <summary>
        /// Reference to the object that will give us acces to http context in classes that normally do not have it.
        /// </summary>
        private static IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Store the httpContextAccessor once per app
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Access current http context
        /// </summary>
        public static HttpContext HttpContext
        {
            get
            {
                return _httpContextAccessor.HttpContext;
            }
        }

    }
}
