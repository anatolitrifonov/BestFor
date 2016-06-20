using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Reflection;

namespace BestFor.TagHelpers
{
    /// <summary>
    /// Yakor = Anchor in Russian.
    /// This is implementation of anchor tag that builds link to controller and action adding culture in front of them.
    /// Default culture is en-US.
    /// End result is /culture/controller/action?querystring
    /// </summary>
    [HtmlTargetElement("yakor", Attributes = "y-action")]
    [HtmlTargetElement("yakor", Attributes = "y-controller")]
    [HtmlTargetElement("yakor", Attributes = "y-culture")]
    [HtmlTargetElement("yakor", Attributes = "y-querystring")]
    [HtmlTargetElement("yakor", Attributes = "y-class")]
    public class YakorTagHelper : TagHelper
    {
        /// <summary>
        /// Culture
        /// </summary>
        [HtmlAttributeName("y-culture")]
        public string Culture { get; set; }

        /// <summary>
        /// Controller
        /// </summary>
        [HtmlAttributeName("y-controller")]
        public string Controller { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        [HtmlAttributeName("y-action")]
        public string Action { get; set; }

        /// <summary>
        /// Any anonymous object example new { answerId = 56 }
        /// Will get converted to ?answerId=56
        /// </summary>
        [HtmlAttributeName("y-querystring")]
        public object RouteValues { get; set; }

        /// <summary>
        /// Class
        /// </summary>
        [HtmlAttributeName("y-class")]
        public string Class { get; set; }

        /// <summary>
        /// Method controls what the tag helper does when executed
        /// </summary>
        /// <param name="context">information associated with the execution of the current HTML tag</param>
        /// <param name="output"> a stateful HTML element representative of the original source used to generate an HTML tag and content</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string querystring = null;
            // RouteValues is an anonymous type
            if (RouteValues != null)
            {
                var pairs = RouteValues.GetType().GetProperties()
                    .Select(x => x.Name + "=" + x.GetValue(RouteValues, null));
                querystring = string.Join("&", pairs);
            }
            if (string.IsNullOrEmpty(Culture) || string.IsNullOrWhiteSpace(Culture)) Culture = "en-US";

            output.Attributes.SetAttribute("href", "/" + Culture + "/" + Controller + "/" + Action +
                (querystring == null ? string.Empty : "?" + querystring));
            output.TagName = "a";    // Replaces <yakor> with <a> tag
            if (!string.IsNullOrEmpty(Class) && !string.IsNullOrWhiteSpace(Class))
                output.Attributes.SetAttribute("class", Class);

            // output.Content.SetContent(address);
        }
    }
}
