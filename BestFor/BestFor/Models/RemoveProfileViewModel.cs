using System.ComponentModel.DataAnnotations;
using R = BestFor.Resources.BestResourcer;


namespace BestFor.Models
{
    /// <summary>
    /// Model/Dto object for account removal page.
    /// </summary>
    /// <remarks>
    /// BestResourcer is used for localization.
    /// [Required] ErrorMessageResourceName for property XYZ iz set to AnnotationErrorMessageRequiredXYZ
    /// [EmailAddress] ErrorMessageResourceName for ANY property is set to AnnotationValidationMessageEmailAddress
    /// [Display] Name for property XYZ iz set to AnnotationDisplayNameXYZ
    /// [StringLength] ErrorMessageResourceName for property XYZ iz set to AnnotationErrorMessageStringLength<MaxLength>X<MinLenght>XYZ
    /// ResourceType = typeof(BestResourcer)
    /// ErrorMessageResourceType = typeof(BestResourcer)
    /// Every Annotation<something> must exist as a property in BestResourcer
    /// </remarks>
    public class RemoveProfileViewModel
    {
        [Required(ErrorMessageResourceName = "AnnotationErrorMessageRequiredReason", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "AnnotationDisplayNameReason", ResourceType = typeof(R))]
        [StringLength(1000, MinimumLength = 3,
            ErrorMessageResourceName = "AnnotationErrorMessageStringLength1000X3Reason", ErrorMessageResourceType = typeof(R))]
        public string Reason { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }
    }
}
