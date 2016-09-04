namespace BestFor.Dto
{
    public class SystemStateDto
    {
        public string AnswersCacheStatus { get; set; }

        public int AnswersCacheNumberItems { get; set; }

        public string SuggestionsCacheStatus { get; set; }

        public int SuggestionsCacheNumberItems { get; set; }

        public string BadWordsCacheStatus { get; set; }

        public int BadWordsCacheNumberItems { get; set; }

        public string UsersCacheStatus { get; set; }

        public int UsersCacheNumberItems { get; set; }

        public string AnswersDescriptionCacheStatus { get; set; }

        public int AnswersDescriptionCacheNumberItems { get; set; }
    }
}
