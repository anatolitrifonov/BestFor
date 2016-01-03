using System.Text.RegularExpressions;
using BestFor.Dto;

namespace BestFor.Services
{
    public class LinkingHelper
    {
        /// <summary>
        /// Convert answer to searchable and readable url
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public static string ConvertAnswerToUrl(AnswerDto answer)
        {
            return string.Format("/best {0} for {1} is {2}", answer.LeftWord, answer.RightWord, answer.Phrase).Replace(" ", "-");
        }

        public static string ConvertAnswerToText(AnswerDto answer)
        {
            return string.Format("Best {0} for {1} is {2}", answer.LeftWord, answer.RightWord, answer.Phrase);
        }

        /// <summary>
        /// Try to parse answer from the url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AnswerDto ParseUrlToAnswer(string url)
        {
            const int GROUP_LEFT_WORD = 1;
            const int GROUP_RIGHT_WORD = 2;
            const int GROUP_PHRASE_WORD = 3;
            const int NUMBER_GROUPS = 4;
            if (string.IsNullOrEmpty(url)) return null;
            if (string.IsNullOrWhiteSpace(url)) return null;

            var matches = Regex.Matches(url.Replace("-", " "), "^/best\\s+(.*)\\s+for\\s+(.*)\\s+is\\s+(.*)");
            if (matches == null) return null; // Should not be null. Could be count zero but not null.
            if (matches.Count == 0) return null;
            if (matches[0].Groups.Count != NUMBER_GROUPS) return null;
            var answer = new AnswerDto()
            {
                LeftWord = matches[0].Groups[GROUP_LEFT_WORD].Value,
                RightWord = matches[0].Groups[GROUP_RIGHT_WORD].Value,
                Phrase = matches[0].Groups[GROUP_PHRASE_WORD].Value
            };
            return answer;
        }
    }
}
