using Microsoft.AspNet.Authorization;
using BestFor.Dto;
using Microsoft.AspNet.Mvc;
using BestFor.Services.Services;
using System.Threading.Tasks;

namespace BestFor.Controllers
{
    /// <summary>
    /// Controller to add detailed descriptions for the answers.
    /// </summary>
    public class AnswerDescriptionController : Controller
    {
        private IAnswerDescriptionService _answerDescriptionService;
        private IProfanityService _profanityService;

        public AnswerDescriptionController(IAnswerDescriptionService answerDescriptionService, IProfanityService profanityService)
        {
            _answerDescriptionService = answerDescriptionService;
            _profanityService = profanityService;
        }


        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AddDescription(int answerId = 0)
        {
            //if (userId == null || code == null)
            //{
            //    return View("Error");
            //}
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    return View("Error");
            //}
            //var result = await _userManager.ConfirmEmailAsync(user, code);
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
            var model = new AnswerDescriptionDto() { AnswerId = answerId };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDescription(AnswerDescriptionDto answerDescription)
        {
            // Basic checks first
            if (answerDescription == null || answerDescription.AnswerId <= 0 ||
                string.IsNullOrEmpty(answerDescription.Description) ||
                string.IsNullOrWhiteSpace(answerDescription.Description)) return View("Error");

            // This might throw exception if there was a header but invalid. But if someone is just messing with us we will return nothing.
            // if (!ParseAntiForgeryHeader()) return SetErrorMessage(result, "Antiforgery issue");

            // Let's first check for profanities.
            var profanityCheckResult = _profanityService.CheckProfanity(answerDescription.Description);
            if (profanityCheckResult.HasIssues)
            {
                // answer.ErrorMessage = profanityCheckResult.ErrorMessage;
                return View("Error");
            }

            // Add answer description
            //var addedAnswerDescription = await _answerDescriptionService.AddAnswerDescription(answerDescription);
            //result.Answer = addedAnswer.ToDto();
            //return result;

            return RedirectToAction("Index", "Home");


            //if (ModelState.IsValid)
            //{
            //    db.AddToMovies(newMovie);
            //    db.SaveChanges();

            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return View(newMovie);
            //}
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AddedDescription(int descriptionId = 0)
        {
            //if (userId == null || code == null)
            //{
            //    return View("Error");
            //}
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    return View("Error");
            //}
            //var result = await _userManager.ConfirmEmailAsync(user, code);
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
            var model = new AnswerDescriptionDto() { AnswerId = descriptionId };

            return View(model);
        }
    }
}
