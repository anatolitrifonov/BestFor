using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using BestFor.Controllers;
using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Fakes;
using BestFor.Services.Cache;
using BestFor.Services.DataSources;
using BestFor.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using BestFor.UnitTests.Testables;

namespace BestFor.UnitTests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class VoteControllerTests
    {
        /// <summary>
        /// Constructor sets up the needed data.
        /// </summary>
        private class TestSetup
        {
            //public SuggestionService SuggestionService;
            //public FakeSuggestions FakeSuggestions;
            //public Mock<ICacheManager> CacheMock;
            //public Repository<Suggestion> Repository;
            //public TestLoggerFactory TestLoggerFactory;
            //public TestLogger<SuggestionService> TestLogger;

            public TestSetup()
            {
                //var dataContext = new FakeDataContext();
                //Repository = new Repository<Suggestion>(dataContext);
                //CacheMock = new Mock<ICacheManager>();

                ////LoggerMock = new Mock<ILogger<SuggestionService>>();
                ////LoggerMock.Setup(x => x.LogInformation(It.IsAny<string>()));
                //// LoggerMock.Setup(x => x.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()));

                //TestLogger = new TestLogger<SuggestionService>();
                //TestLoggerFactory = new TestLoggerFactory();

                //SuggestionService = new SuggestionService(CacheMock.Object, Repository, TestLoggerFactory);
                //FakeSuggestions = dataContext.EntitySet<Suggestion>() as FakeSuggestions;
            }
        }

        [Fact]
        public async Task VoteController_VoteAnswer_AddsVote()
        {
            // Arrange
            var dataContext = new FakeDataContext();
            var testLoggerFactory = new TestLoggerFactory();
            var userManager = new UserManager<ApplicationUser>(dataContext, null, null, null, null, null, null, null, null);
            var identity = new GenericIdentity("asd");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "ASD"));
            var principal = new GenericPrincipal(identity, null);
            var httpContext = new DefaultHttpContext() { User = principal };
            var controllerContext = new ControllerContext() { HttpContext = httpContext };

            // Setup test data
            var vote = new AnswerVoteDto() { AnswerId = 1 };

            // Setup vote service
            var voteServiceMock = new Mock<IVoteService>();
            voteServiceMock.Setup(x => x.VoteAnswer(vote)).Returns(1);

            // Setup resource service
            var resourceServiceMock = new Mock<IResourcesService>();
            resourceServiceMock.Setup(x => x.GetString(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("A");

            var controller = new VoteController(userManager, voteServiceMock.Object, resourceServiceMock.Object, testLoggerFactory)
            {
                ControllerContext = controllerContext
            };

            // Act
            var result = await controller.VoteAnswer(vote.AnswerId);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            // Controller did call the vote service
            voteServiceMock.Verify(x => x.VoteAnswer(It.IsAny<AnswerVoteDto>()), Times.Once());
            // Controller did call the resource service
            resourceServiceMock.Verify(x => x.GetString(BaseApiController.DEFAULT_CULTURE, Lines.THANK_YOU_FOR_VOTING), Times.Once());
        }
    }
}
