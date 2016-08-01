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
        private readonly IResourcesService _resourcesService;

        public FlagController(UserManager<ApplicationUser> userManager, IFlagService flagService, IResourcesService resourcesService,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _flagService = flagService;
            _resourcesService = resourcesService;
            _logger = loggerFactory.CreateLogger<FlagController>();
            _userManager = userManager;
            _logger.LogInformation("created FlagController");
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

            // Read the reason
            var reason = await _resourcesService.GetString(this.Culture, Lines.THANK_YOU_FOR_FLAGING);

            return RedirectToAction("ShowAnswer", "AnswerAction", new { answerId = answerId, reason = reason });
        }

        [HttpGet]
        public async Task<IActionResult> FlagAnswerDescription(int answerDescriptionId = 0)
        {
            _logger.LogDebug("FlagAnswerDescription answerDescriptionId = " + answerDescriptionId);

            var answerId = answerDescriptionId; // <- not good

            // Only do something is answer id is not zero
            if (answerDescriptionId != 0)
            {
                // this does return answerId
                answerId = await _flagService.FlagAnswerDescription(
                    new AnswerDescriptionFlagDto() { AnswerDescriptionId = answerDescriptionId, UserId = _userManager.GetUserId(User) }
                    );
            }

            // Read the reason
            var reason = await _resourcesService.GetString(this.Culture, Lines.THANK_YOU_FOR_FLAGING);

            return RedirectToAction("ShowAnswer", "AnswerAction", new { answerId = answerId, reason = reason });
        }
    }
}
