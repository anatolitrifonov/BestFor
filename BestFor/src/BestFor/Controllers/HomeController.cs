using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services;
using BestFor.Services.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
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
        private IAnswerService _answerService;
        private IAnswerDescriptionService _answerDescriptionService;
        private IResourcesService _resourcesService;
        private ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private IVoteService _voteService;

        public HomeController(IAnswerService answerService, IAnswerDescriptionService answerDescriptionService,
            IResourcesService resourcesService, UserManager<ApplicationUser> userManager, IVoteService voteService,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
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
        public IActionResult Index()
        {
            var model = new HomePageDto();

            model.TopToday.Answers = _answerService.FindAnswersTrendingToday();

            model.Culture = this.Culture;

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
            var commonStrings = _resourcesService.GetCommonStrings(culture);
            var answer = LinkingHelper.ParseUrlToAnswer(commonStrings, requestPath);
            // Were we able to parse?
            if (answer == null) RedirectToAction("Index");
            // Let's try to find that answer
            answer = _answerService.FindExact(answer.LeftWord, answer.RightWord, answer.Phrase);
            // Go to home index if not found
            if (answer == null) RedirectToAction("Index");
            // Get data
            return View(await FillInDetails(answer, _answerDescriptionService, _userManager, _voteService, _resourcesService, culture));
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
            UserManager<ApplicationUser> userManager, IVoteService  voteService, IResourcesService resourcesService, string culture)
        {
            // Load answer descriptions
            var descriptions = answerDescriptionService.FindByAnswerId(answer.Id);
            // Fill in result
            var data = new AnswerDetailsDto()
            {
                Answer = answer,
                CommonStrings = resourcesService.GetCommonStrings(culture),
                Descriptions = descriptions,
                UserDisplayName = await GetUserDisplayName(answer.UserId, userManager),
                NumberVotes = await voteService.CountAnswerVotes(answer.Id)
            };

            return data;
        }

        public static async Task<string> GetUserDisplayName(string userId, UserManager<ApplicationUser> userManager)
        {
            var result = "Anonymous";
            if (userId == null) return result;
            // Get user details.
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return result;
            if (user.DisplayName == null) return user.UserName;
            if (user.DisplayName == string.Empty) return user.UserName;
            return user.DisplayName;
        }
    }
}
