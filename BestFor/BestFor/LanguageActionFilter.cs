using Microsoft.AspNetCore.Mvc.Filters;

namespace BestFor
{
    /// <summary>
    /// This action filter saves current request culture in ViewBag so that it is available in views for rendering.
    /// </summary>
    public class LanguageActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var controller = context.Controller as BestFor.Controllers.BaseApiController;
            // Make sure we got the right controller
            if (controller != null)
            {
                // Controller parses the culture.
                // Stick it into view bag so that view can use it for localized strings.
                controller.ViewBag.BestForCulture = controller.Culture;
            }

            base.OnResultExecuting(context);
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
           //            string culture = context.RouteData.Values["culture"].ToString();
           //        //    _logger.LogInformation($"Setting the culture from the URL: {culture}");

            //#if DNX451
            //            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            //            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            //#else
            //            CultureInfo.CurrentCulture = new CultureInfo(culture);
            //            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            //#endif
            base.OnActionExecuting(context);
        }
    }
}
