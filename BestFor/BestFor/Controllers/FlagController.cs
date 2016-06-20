using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
using BestFor.Services.Services;
using BestFor.Dto;
using BestFor.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BestFor.Controllers
{
    /// <summary>
    /// Allows user to flag data if he sees something wrong with it.
    /// 
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    [Authorize]
    public class FlagController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IFlagService _flagService;
        private readonly ILogger _logger;

        public FlagController(IFlagService flagService, ILoggerFactory loggerFactory)
        {
            _flagService = flagService;
            _logger = loggerFactory.CreateLogger<FlagController>();
        }

        [HttpGet]
        public async Task<IActionResult> FlagAnswer(int answerId = 0)
        {
            _logger.LogDebug("FlagAnswer answerId = " + answerId);

            // Only do something is answer id is not zero
            if (answerId != 0)
            {
                await _flagService.FlagAnswer(new AnswerFlagDto() { AnswerId = answerId, UserId = _userManager.GetUserId(User) } );
            }

            return RedirectToAction("ConfirmFlag");
        }

        [HttpGet]
        public async Task<IActionResult> FlagAnswerDescription(int answerDescriptionId = 0)
        {
            _logger.LogDebug("FlagAnswerDescription answerDescriptionId = " + answerDescriptionId);

            // Only do something is answer id is not zero
            if (answerDescriptionId != 0)
            {
                await _flagService.FlagAnswerDescription(
                    new AnswerDescriptionFlagDto() { AnswerDescriptionId = answerDescriptionId, UserId = _userManager.GetUserId(User) }
                    );
            }

            return RedirectToAction("ConfirmFlag");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmFlag()
        {
            return await Task.FromResult(View());
        }
    }
}
