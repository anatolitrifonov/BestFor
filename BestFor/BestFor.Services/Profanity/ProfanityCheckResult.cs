using System;

namespace BestFor.Services.Profanity
{
    public class ProfanityCheckResult
    {
        public string ProfanityWord { get; set; }

        public bool HasBadCharacters { get; set; }

        public bool NoData { get; set; }

        public string ErrorMessage { get; set; }

        public bool HasIssues
        {
            get
            {
                return NoData || HasBadCharacters || ProfanityWord != null;
            }
        }

        public string DefaultErrorMessage
        {
            get
            {
                if (NoData) return "No data.";
                if (HasBadCharacters) return "Data contains bad characters.";
                if (ProfanityWord != null) return "Profanity found \"" + ProfanityWord + "\".";
                throw new Exception("HasIssues should be checked before calling this met");
            }
        }
    }
}
