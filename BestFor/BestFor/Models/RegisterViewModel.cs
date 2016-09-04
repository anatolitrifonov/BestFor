using System.ComponentModel.DataAnnotations;
using R = BestFor.Resources.BestResourcer;


namespace BestFor.Models
{
    /// <summary>
    /// Model/Dto object for user registration page.
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
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "AnnotationErrorMessageRequiredEmail", ErrorMessageResourceType = typeof(R))]
        [EmailAddress(ErrorMessageResourceName = "AnnotationValidationMessageEmailAddress", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "AnnotationDisplayNameEmail", ResourceType = typeof(R))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "AnnotationErrorMessageRequiredPassword", ErrorMessageResourceType = typeof(R))]
        [StringLength(100, MinimumLength = 6,
            ErrorMessageResourceName = "AnnotationErrorMessageStringLength100X6Password", ErrorMessageResourceType = typeof(R))]
        [DataType(DataType.Password)]
        [Display(Name = "AnnotationDisplayNamePassword", ResourceType = typeof(R))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "AnnotationDisplayNameConfirmPassword", ResourceType = typeof(R))]
        [Compare("Password", ErrorMessageResourceName = "AnnotationErrorMessageComparePassword", ErrorMessageResourceType = typeof(R))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "AnnotationErrorMessageRequiredUserName", ErrorMessageResourceType = typeof(R))]
        [StringLength(100, MinimumLength = 4,
            ErrorMessageResourceName = "AnnotationErrorMessageStringLength100X4UserName", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "AnnotationDisplayNameUserName", ResourceType = typeof(R))]
        public string UserName { get; set; }

        [StringLength(100, MinimumLength = 3,
            ErrorMessageResourceName = "AnnotationErrorMessageStringLength100X3DisplayName", ErrorMessageResourceType = typeof(R))]
        [Display(Name = "AnnotationDisplayNameDisplayName", ResourceType = typeof(R))]
        public string DisplayName { get; set; }
    }
}
