using BestFor.Dto;
using BestFor.Services.Services;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BestFor.Controllers
{
    /// <summary>
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// Controller itself adds extended descriptions to the answers.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    public class AnswerActionController : BaseApiController
    {
        private readonly IAnswerDescriptionService _answerDescriptionService;
        private readonly IAnswerService _answerService;
        private readonly IProfanityService _profanityService;
        private readonly IResourcesService _resourcesService;
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IVoteService _voteService;

        public AnswerActionController(IAnswerDescriptionService answerDescriptionService, IProfanityService profanityService,
            IAnswerService answerService, IResourcesService resourcesService, IUserService userService,
            IVoteService voteService, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _answerDescriptionService = answerDescriptionService;
            _profanityService = profanityService;
            _answerService = answerService;
            _resourcesService = resourcesService;
            _voteService = voteService;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.LogInformation("created AnswerActionController");
        }

        /// <summary>
        /// Loads an answer by id to show full details.
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowAnswer(int answerId = 0)
        {
            var culture = this.Culture;
            var requestPath = Request.Path.Value;
            // cut the culture
            var cultureBegining = "/" + culture;
            if (requestPath.StartsWith(cultureBegining)) requestPath = requestPath.Substring(cultureBegining.Length);
            // Now try to parse the request path into known words.
            // var commonStrings = await _resourcesService.GetCommonStrings(culture);
            // Load the answer.
            var answer = await _answerService.FindById(answerId);

            return View("MyContent",
                await HomeController.FillInDetails(answer, _answerDescriptionService, _userService, _voteService, _resourcesService, culture));
        }

        /// <summary>
        /// Load view to add answer description.
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AddDescription(int answerId = 0)
        {
            // Let's load the answer.
            // The hope is that service will not have to go to the database and load answer from cache.
            // But please look at the servise implementation for details.
            var answer = await _answerService.FindById(answerId);

            // Model is basically empty at this point.
            var model = new AnswerDescriptionDto() { Answer = answer, AnswerId = answerId };

            return View(model);
        }

        /// <summary>
        /// Add description for the answer.
        /// todo: figure out how to protect from spam posts
        /// </summary>
        /// <param name="answerDescription"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDescription(AnswerDescriptionDto answerDescription)
        {
            // Basic checks first
            if (answerDescription == null || answerDescription.AnswerId <= 0 ||
                string.IsNullOrEmpty(answerDescription.Description) ||
                string.IsNullOrWhiteSpace(answerDescription.Description)) return View("Error");

            // todo: figure out how to protect from spam posts besides antiforgery

            // Let's first check for profanities.
            var profanityCheckResult = await _profanityService.CheckProfanity(answerDescription.Description);
            if (profanityCheckResult.HasIssues)
            {
                // answer.ErrorMessage = profanityCheckResult.ErrorMessage;
                // todo: settle on displaying errors from controller posts and gets
                return View("Error");
            }

            // If user is logged in let's add him to the object
            // This will return null if user is not logged in and this is OK.
            answerDescription.UserId = User.GetUserId();

            // Add answer description
            var addedAnswerDescription = await _answerDescriptionService.AddAnswerDescription(answerDescription);

            // Redirect to show the answer. This will prevent user refreshing the page.
            return RedirectToAction("ShowAnswer", new { answerId = answerDescription.AnswerId });
        }
    }
}
