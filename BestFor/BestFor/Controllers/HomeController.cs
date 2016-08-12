using BestFor.Dto;
using BestFor.Services;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BestFor.Controllers
{
    /// <summary>
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    public class HomeController : BaseApiController
    {
        /// <summary>
        /// Constructor injected answer service. Used for loading the answers.
        /// </summary>
        private readonly IAnswerService _answerService;
        private readonly IAnswerDescriptionService _answerDescriptionService;
        private readonly IResourcesService _resourcesService;
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IVoteService _voteService;

        public HomeController(IAnswerService answerService, IAnswerDescriptionService answerDescriptionService,
            IResourcesService resourcesService, IUserService userService, IVoteService voteService,
            ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _answerService = answerService;
            _answerDescriptionService = answerDescriptionService;
            _resourcesService = resourcesService;
            _voteService = voteService;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.LogInformation("created HomeController");
        }

        /// <summary>
        /// Default home page view.
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        public async Task<IActionResult> Index(string reason = null)
        {
            var model = new HomePageDto();

            model.TopToday.Answers = await _answerService.FindAnswersTrendingToday();

            model.Reason = reason;

            // Check if we need to debug react
            model.DebugReactControls = ReadUrlParameterAsBoolean("debugreact");

            return View(model);
        }

        /// <summary>
        /// This view will be rendered if answer is in the URL string.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MyContent()
        {
            var culture = this.Culture;
            var requestPath = Request.Path.Value;
            // cut the culture
            var cultureBegining = "/" + culture;
            if (requestPath.StartsWith(cultureBegining)) requestPath = requestPath.Substring(cultureBegining.Length);
            // Now try to parse the request path into known words.
            var commonStrings = await _resourcesService.GetCommonStrings(culture);
            var answer = LinkingHelper.ParseUrlToAnswer(commonStrings, requestPath);
            // Were we able to parse?
            if (answer == null) return RedirectToAction("Index");
            // Let's try to find that answer
            answer = await _answerService.FindExact(answer.LeftWord, answer.RightWord, answer.Phrase);
            // Go to home index if not found
            if (answer == null) return RedirectToAction("Index");
            // Get data
            return View(await FillInDetails(answer, _answerDescriptionService, _userService, _voteService, _resourcesService, culture));
        }

        /// <summary>
        /// Fill in answer details for content page.
        /// Reuse the logic between controllers. Also used by AnswerActionController.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="answerDescriptionService"></param>
        /// <param name="userManager"></param>
        /// <param name="voteService"></param>
        /// <param name="resourcesService"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static async Task<AnswerDetailsDto> FillInDetails(AnswerDto answer, IAnswerDescriptionService answerDescriptionService,
            IUserService userService, IVoteService  voteService, IResourcesService resourcesService, string culture)
        {
            // Load answer descriptions
            // Have to do the list otherwise setting description.UserDisplayName below will not work.
            var searchResult = await answerDescriptionService.FindByAnswerId(answer.Id);
            List<AnswerDescriptionDto> descriptions = searchResult == null ? null : searchResult.ToList();

            // Set the username for each description
            if (descriptions != null)
            {
                foreach (var description in descriptions)
                {
                    description.UserDisplayName = await GetUserDisplayName(description.UserId, userService);
                }
            }

            // Fill in result
            var data = new AnswerDetailsDto()
            {
                Answer = answer,
                CommonStrings = await resourcesService.GetCommonStrings(culture),
                Descriptions = descriptions,
                UserDisplayName = await GetUserDisplayName(answer.UserId, userService),
                NumberVotes = await voteService.CountAnswerVotes(answer.Id)
            };

            return data;
        }

        /// <summary>
        /// Generate or find user display name from user id using user service.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        private static async Task<string> GetUserDisplayName(string userId, IUserService userService)
        {
            var result = "Anonymous";
            if (userId == null || userService == null) return result;
            if (string.IsNullOrEmpty(userId)) return result;
            if (string.IsNullOrWhiteSpace(userId)) return result;
            // Get user details.
            var user = await userService.FindByIdAsync(userId);
            if (user == null) return result;
            if (user.DisplayName == null) return user.UserName;
            if (user.DisplayName == string.Empty) return user.UserName;
            return user.DisplayName;
        }
    }
}
