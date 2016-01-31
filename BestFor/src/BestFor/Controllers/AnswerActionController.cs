using Microsoft.AspNet.Authorization;
using BestFor.Dto;
using Microsoft.AspNet.Mvc;
using BestFor.Services.Services;
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
        private IAnswerDescriptionService _answerDescriptionService;
        private IAnswerService _answerService;
        private IProfanityService _profanityService;

        public AnswerActionController(IAnswerDescriptionService answerDescriptionService, IProfanityService profanityService,
            IAnswerService answerService)
        {
            _answerDescriptionService = answerDescriptionService;
            _profanityService = profanityService;
            _answerService = answerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowAnswer(int answerId = 0)
        {
            // Let's load the answer.
            // The hope is that service will not have to go to the database and load answer from cache.
            // But please look at the servise implementation for details.
            var answer = _answerService.FindById(answerId);

            // Model is basically empty at this point.
            var model = new AnswerDescriptionDto() { Answer = answer, AnswerId = answerId };

            return View(model);
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
            var answer = _answerService.FindById(answerId);

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
        public async Task<IActionResult> AddDescription(AnswerDescriptionDto answerDescription)
        {
            // Basic checks first
            if (answerDescription == null || answerDescription.AnswerId <= 0 ||
                string.IsNullOrEmpty(answerDescription.Description) ||
                string.IsNullOrWhiteSpace(answerDescription.Description)) return View("Error");

            // todo: figure out how to protect from spam posts
            // This might throw exception if there was a header but invalid. But if someone is just messing with us we will return nothing.
            // if (!ParseAntiForgeryHeader()) return SetErrorMessage(result, "Antiforgery issue");

            // Let's first check for profanities.
            var profanityCheckResult = _profanityService.CheckProfanity(answerDescription.Description);
            if (profanityCheckResult.HasIssues)
            {
                // answer.ErrorMessage = profanityCheckResult.ErrorMessage;
                // todo: settle on displaying errors from controller posts and gets
                return View("Error");
            }

            // Add answer description
            var addedAnswerDescription = await _answerDescriptionService.AddAnswerDescription(answerDescription);

            // Redirect to show the answer. This will prevent user refreshing the page.
            return RedirectToAction("ShowAnswer", new { answerId = answerDescription.AnswerId });
        }
    }
}
