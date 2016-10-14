using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BestFor.Controllers
{
    /// <summary>
    /// Allows user to vote for answers and answer descriptions.
    /// 
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    [Authorize]
    public class VoteController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IVoteService _voteService;
        private readonly ILogger _logger;
        private readonly IResourcesService _resourcesService;

        public VoteController(UserManager<ApplicationUser> userManager, IVoteService voteService, IResourcesService resourcesService,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _voteService = voteService;
            _resourcesService = resourcesService;
            _logger = loggerFactory.CreateLogger<VoteController>();
            _logger.LogInformation("created VoteController");
        }

        [HttpGet]
        public async Task<IActionResult> VoteAnswer(int answerId = 0)
        {
            _logger.LogDebug("VoteAnswer answerId = " + answerId);

            // Only do something is answer id is not zero
            if (answerId != 0)
            {
                var userId = _userManager.GetUserId(User);
                _voteService.VoteAnswer(new AnswerVoteDto() { AnswerId = answerId, UserId = _userManager.GetUserId(User) } );
            }

            // Read the reason
            var reason = await _resourcesService.GetString(this.Culture, Lines.THANK_YOU_FOR_VOTING);

            return RedirectToAction("ShowAnswer", "AnswerAction", new { answerId = answerId, reason = reason });
        }

        [HttpGet]
        public async Task<IActionResult> VoteAnswerDescription(int answerDescriptionId = 0)
        {
            _logger.LogDebug("VoteAnswerDescription answerDescriptionId = " + answerDescriptionId);

            var answerId = answerDescriptionId; // <- not good

            // Only do something is answer id is not zero
            if (answerDescriptionId != 0)
            {
                // this does return answerId
                answerId = _voteService.VoteAnswerDescription(
                    new AnswerDescriptionVoteDto() { AnswerDescriptionId = answerDescriptionId, UserId = _userManager.GetUserId(User) }
                    );
            }

            // Read the reason
            var reason = await _resourcesService.GetString(this.Culture, Lines.THANK_YOU_FOR_VOTING);

            return RedirectToAction("ShowAnswer", "AnswerAction", new { answerId = answerId, reason = reason });
        }
    }
}
