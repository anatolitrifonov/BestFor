using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Model for home index controller.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HomePageDto
    {
        public AnswersDto TopToday { get; set; } = new AnswersDto();

        // Reason user got redirected to the page.
        public string Reason { get; set; }

        // Keyword that was used on the home page
        public string Keyword { get; set; }

        public string HeaderText { get; set; }

        // If set to true React controls will go into debug mode
        public bool DebugReactControls { get; set; } = false;
    }
}
