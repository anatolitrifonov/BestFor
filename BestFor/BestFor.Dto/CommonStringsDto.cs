using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Used by resource service to pass around commongly known strings
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CommonStringsDto
    {
        public string Best { get; set; } = "Best";

        public string For { get; set; } = "for";

        public string Is { get; set; } = "is";

        public string FlagLower { get; set; } = "flag";

        public string FlagUpper { get; set; } = "Flag";

        public string VoteLower { get; set; } = "vote";

        public string VoteUpper { get; set; } = "Vote";

        public string DescribeLower { get; set; } = "describe";

        public string DescribeUpper { get; set; } = "Describe";

        public string MoreLower { get; set; } = "more";

        public string MoreUpper { get; set; } = "More";

        public string PostersUpper { get; set; } = "Posters";
    }
}
