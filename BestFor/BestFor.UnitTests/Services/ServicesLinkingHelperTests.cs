using BestFor.Services;
using BestFor.Dto;
using Xunit;
// using Autofac;

namespace BestFor.UnitTests.Services
{
    /// <summary>
    /// Unit tests for DefaultSuggestions object
    /// </summary>
    public class ServicesLinkingHelperTests // : BaseTest
    {
        [Fact]
        public void LinkingHelper_ParseAnswerUrl_Success()
        {
            string data = "/best-girl-for-the-dance-is-alice";

            var answer = LinkingHelper.ParseUrlToAnswer(data);

            Assert.Equal(answer.LeftWord, "girl");
            Assert.Equal(answer.RightWord, "the dance");
            Assert.Equal(answer.Phrase, "alice");
        }

        [Fact]
        public void LinkingHelper_ParseAnswerUrl_Fail()
        {
            string data = "/best-girl-f or-the-dance-is-alice";

            var answer = LinkingHelper.ParseUrlToAnswer(data);

            Assert.Null(answer);
        }
    }
}
