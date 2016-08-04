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
    }
}
