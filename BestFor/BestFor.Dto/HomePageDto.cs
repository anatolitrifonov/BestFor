namespace BestFor.Dto
{
    /// <summary>
    /// Model for home index controller.
    /// </summary>
    public class HomePageDto
    {
        public AnswersDto TopToday { get; set; } = new AnswersDto();

        // Reason user got redirected to the page.
        public string Reason { get; set; }

        // If set to true React controls will go into debug mode
        public bool DebugReactControls { get; set; } = false;
    }
}
