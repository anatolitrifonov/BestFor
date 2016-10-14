using BestFor.Common;
using BestFor.Dto;
using BestFor.Services;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IOptions<AppSettings> _appSettings;

        public HomeController(IAnswerService answerService, IAnswerDescriptionService answerDescriptionService,
            IResourcesService resourcesService, IUserService userService, IVoteService voteService,
            ILoggerFactory loggerFactory, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _answerService = answerService;
            _answerDescriptionService = answerDescriptionService;
            _resourcesService = resourcesService;
            _voteService = voteService;
            _appSettings = appSettings;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.LogInformation("created HomeController");
        }

        /// <summary>
        /// Default home page view.
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        public async Task<IActionResult> Index(string reason = null, string searchPhrase = null)
        {
            var model = new HomePageDto();

            // Use the search service if search phrase is passed
            if (!string.IsNullOrEmpty(searchPhrase) && !string.IsNullOrWhiteSpace(searchPhrase))
            {
                model.TopToday.Answers = await _answerService.FindLastAnswers(searchPhrase);
                model.Keyword = searchPhrase;
                model.HeaderText = await _resourcesService.GetString(this.Culture, Lines.SEARCH_RESULTS_FOR) +
                    ": " + searchPhrase;
            }
            else
            {
                model.TopToday.Answers = await _answerService.FindAnswersTrendingToday();
                model.HeaderText = await _resourcesService.GetString(this.Culture, Lines.TRENDING_TODAY);
            }

            model.Reason = reason;

            // Check if we need to debug react
            model.DebugReactControls = ReadUrlParameterAsBoolean(DEBUG_REACTJS_URL_PARAMETER_NAME);

            return View(model);
        }

        /// <summary>
        /// Show the view that allows adding new opinions.
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        public IActionResult Contribute(string reason = null)
        {
            // async Task<IActionResult>
            var model = new HomePageDto();

            // We do not need trending today on contribute page
            // model.TopToday.Answers = await _answerService.FindAnswersTrendingToday();
            model.Reason = reason;

            // Check if we need to debug react
            model.DebugReactControls = ReadUrlParameterAsBoolean(DEBUG_REACTJS_URL_PARAMETER_NAME);

            return View(model);
        }

        /// <summary>
        /// This view will be rendered if answer is in the URL string.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MyContent()
        {
            var culture = this.Culture;
            var requestPath = this.RequestPathNoCulture;
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
            var answerDetails = await FillInDetails(answer, _answerDescriptionService, _userService, _voteService, _resourcesService,
                culture, _appSettings.Value.FullDomainAddress);

            // Do a bit more playing with data
            answerDetails.EnableFacebookSharing = _appSettings.Value.EnableFacebookSharing;

            answerDetails.DebugReactControls = ReadUrlParameterAsBoolean(DEBUG_REACTJS_URL_PARAMETER_NAME);

            return View(answerDetails);
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
            IUserService userService, IVoteService  voteService, IResourcesService resourcesService, string culture, string fullDomainName)
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
                    description.UserDisplayName = GetUserDisplayName(description.UserId, userService);
                }
            }

            // Fill in result
            var data = new AnswerDetailsDto()
            {
                Answer = answer,
                CommonStrings = await resourcesService.GetCommonStrings(culture),
                Descriptions = descriptions,
                UserDisplayName = GetUserDisplayName(answer.UserId, userService),
                NumberVotes = voteService.CountAnswerVotes(answer.Id)
            };

            // Fill in link to this page and other usefull data.
            data.ThisAnswerLink = LinkingHelper.ConvertAnswerToUrlWithCulture(culture, data.CommonStrings, answer);
            data.ThisAnswerFullLink = fullDomainName.EndsWith("/") ? fullDomainName.Substring(0, fullDomainName.Length - 1) : fullDomainName + data.ThisAnswerLink;
            data.ThisAnswerText = LinkingHelper.ConvertAnswerToText(data.CommonStrings, answer);
            data.ThisAnswerFullLinkEscaped = System.Uri.EscapeDataString(data.ThisAnswerFullLink);

            return data;
        }

        /// <summary>
        /// Generate or find user display name from user id using user service.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        private static string GetUserDisplayName(string userId, IUserService userService)
        {
            var result = "Anonymous";
            if (userId == null || userService == null) return result;
            if (string.IsNullOrEmpty(userId)) return result;
            if (string.IsNullOrWhiteSpace(userId)) return result;
            // Get user details.
            var user = userService.FindById(userId);
            if (user == null) return result;
            if (user.DisplayName == null) return user.UserName;
            if (user.DisplayName == string.Empty) return user.UserName;
            if (user.IsCancelled) return result; // <-- Anonymous if user is cancelled.
            return user.DisplayName;
        }
    }
}
