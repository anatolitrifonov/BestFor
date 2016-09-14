using BestFor.Services.Services;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace BestFor.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class AnswerServiceTests
    {
        AnswerService _answerService;

        public AnswerServiceTests()
        {
            _answerService = new AnswerService(null, null, null, null);
        }

        [Fact]
        public void AnswerServiceTests_Variations_Returns1()
        {
            var data = _answerService.Variations(null);
            Assert.NotNull(data);
            Assert.Equal(data.Count, 0);

            data = _answerService.Variations("      ");
            Assert.Equal(data.Count, 0);

            data = _answerService.Variations("a");
            Assert.Equal(data.Count, 0);

            data = _answerService.Variations("a ");
            Assert.Equal(data.Count, 1);
            Assert.Equal(data[0], "a-");

            data = _answerService.Variations("a b");
            Assert.Equal(data.Count, 1);
            Assert.Equal(data[0], "a-b");

            data = _answerService.Variations("a b ");
            Assert.Equal(data.Count, 3);
            Assert.Equal(data[0], "a-b ");
            Assert.Equal(data[1], "a b-");
            Assert.Equal(data[2], "a-b-");

            data = _answerService.Variations("a  b");
            Assert.Equal(data.Count, 3);
            Assert.Equal(data[0], "a- b");
            Assert.Equal(data[1], "a -b");
            Assert.Equal(data[2], "a--b");

            data = _answerService.Variations("a b c");
            Assert.Equal(data.Count, 3);
            Assert.Equal(data[0], "a-b c");
            Assert.Equal(data[1], "a b-c");
            Assert.Equal(data[2], "a-b-c");

            data = _answerService.Variations("a b c ");
            Assert.Equal(data.Count, 7);
            Assert.Equal(data[0], "a-b c ");
            Assert.Equal(data[1], "a b-c ");
            Assert.Equal(data[2], "a-b-c ");
            Assert.Equal(data[3], "a b c-");
            Assert.Equal(data[4], "a b-c-");
            Assert.Equal(data[5], "a-b c-");
            Assert.Equal(data[6], "a-b-c-");

            data = _answerService.Variations("a b csd");
            Assert.Equal(data.Count, 3);
            Assert.Equal(data[0], "a-b csd");
            Assert.Equal(data[1], "a b-csd");
            Assert.Equal(data[2], "a-b-csd");

            data = _answerService.Variations("a b csd asdasdas");
            Assert.Equal(data.Count, 7);
            Assert.Equal(data[0], "a-b csd asdasdas");
            Assert.Equal(data[1], "a b-csd asdasdas");
            Assert.Equal(data[2], "a-b-csd asdasdas");
            Assert.Equal(data[3], "a b csd-asdasdas");
            Assert.Equal(data[4], "a b-csd-asdasdas");
            Assert.Equal(data[5], "a-b csd-asdasdas");
            Assert.Equal(data[6], "a-b-csd-asdasdas");
        }
    }
}
