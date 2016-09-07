using System.Threading;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BestFor.Resources
{
    /// <summary>
    /// Implements recource manager. Resources are loaded from database using ResourceService.
    /// Implements loading current culture and current http context.
    /// 
    /// [Required] ErrorMessageResourceName for property XYZ iz set to AnnotationErrorMessageRequiredXYZ
    /// [EmailAddress] ErrorMessageResourceName for ANY property is set to AnnotationValidationMessageEmailAddress
    /// [Display] Name for property XYZ iz set to AnnotationDisplayNameXYZ
    /// [StringLength] ErrorMessageResourceName for property XYZ iz set to AnnotationErrorMessageStringLength<MaxLength>X<MinLenght>XYZ
    /// ResourceType = typeof(BestResourcer)
    /// ErrorMessageResourceType = typeof(BestResourcer)
    /// Every Annotation<something> must exist as a property in BestResourcer
    /// </summary>
    public class BestResourcer
    {
        /// <summary>
        /// I will comment only one or two because the rest is are the same.
        /// 
        /// Usage [Required(ErrorMessageResourceName = "AnnotationErrorMessageRequiredEmail", ErrorMessageResourceType = typeof(R))]
        /// Model property marked by required property like this will use this static property of BestResourcer
        /// to load the error message
        /// Property name maps directly to the key string in the database.
        /// Preferably propertly name should be the same as the key passed to the GetString
        /// </summary>
        public static string AnnotationErrorMessageRequiredEmail { get { return GetString("AnnotationErrorMessageRequiredEmail"); } }

        public static string AnnotationValidationMessageEmailAddress { get { return GetString("AnnotationValidationMessageEmailAddress"); } }

        public static string AnnotationErrorMessageRequiredPassword { get { return GetString("AnnotationErrorMessageRequiredPassword"); } }

        public static string AnnotationErrorMessageStringLength100X6Password { get { return GetString("AnnotationErrorMessageStringLength100X6Password"); } }

        public static string AnnotationDisplayNamePassword { get { return GetString("AnnotationDisplayNamePassword"); } }

        public static string AnnotationDisplayNameConfirmPassword { get { return GetString("AnnotationDisplayNameConfirmPassword"); } }

        public static string AnnotationErrorMessageRequiredUserName { get { return GetString("AnnotationErrorMessageRequiredUserName"); } }

        public static string AnnotationErrorMessageStringLength100X6UserName { get { return GetString("AnnotationErrorMessageStringLength100X6UserName"); } }

        public static string AnnotationErrorMessageStringLength100X6DisplayName { get { return GetString("AnnotationErrorMessageStringLength100X6DisplayName"); } }

        /// <summary>
        /// Usage [Display(Name = "AnnotationDisplayNameUserName", ResourceType = typeof(R))]
        /// 
        /// Normally used for labels on forms. 
        /// </summary>
        public static string AnnotationDisplayNameUserName { get { return GetString("AnnotationDisplayNameUserName"); } }

        public static string AnnotationDisplayNameDisplayName { get { return GetString("AnnotationDisplayNameDisplayName"); } }

        public static string AnnotationDisplayNameEmail { get { return GetString("AnnotationDisplayNameEmail"); } }

        public static string AnnotationErrorMessageComparePassword { get { return GetString("AnnotationErrorMessageComparePassword"); } }

        public static string AnnotationErrorMessageRequiredReason { get { return GetString("AnnotationErrorMessageRequiredReason"); } }

        public static string AnnotationDisplayNameReason { get { return GetString("AnnotationDisplayNameReason"); } }

        public static string AnnotationErrorMessageStringLength1000X3Reason { get { return GetString("AnnotationErrorMessageStringLength1000X3Reason"); } }

        public static string AnnotationErrorMessageStringLength100X4UserName { get { return GetString("AnnotationErrorMessageStringLength100X4UserName"); } }

        public static string AnnotationErrorMessageStringLength100X3DisplayName { get { return GetString("AnnotationErrorMessageStringLength100X3DisplayName"); } }

        private static string GetString(string key)
        {
            // First get the http context
            var httpContext = BestHttpHelper.HttpContext;
            // Can not do anything if there is no context
            if (httpContext == null) return key;

            // get the resources service
            var service = httpContext.RequestServices.GetService(typeof(IResourcesService));
            // Can not do anything if there is no context
            if (service == null) return key;

            // Base controller was supposed to save the current culture from request 
            var currentThreadCulture = Thread.GetData(Thread.GetNamedDataSlot("SavedThreadCulture"));

            string thisCulture = currentThreadCulture == null ? "en-US" : currentThreadCulture.ToString();

            // Use the service to get the string
            var task = ((IResourcesService)service).GetString(thisCulture, key);
            task.Wait();
            var result = task.Result;

            return result == null ? key : result;
        }        
    }
}
