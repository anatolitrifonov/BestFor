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

        public VoteController(IVoteService voteService, ILoggerFactory loggerFactory)
        {
            _voteService = voteService;
            _logger = loggerFactory.CreateLogger<FlagController>();
        }

        [HttpGet]
        public async Task<IActionResult> VoteAnswer(int answerId = 0)
        {
            _logger.LogDebug("VoteAnswer answerId = " + answerId);

            // Only do something is answer id is not zero
            if (answerId != 0)
            {
                await _voteService.VoteAnswer(new AnswerVoteDto() { AnswerId = answerId, UserId = _userManager.GetUserId(User) } );
            }

            return RedirectToAction("ConfirmVote", new { answerId = answerId });
        }

        [HttpGet]
        public async Task<IActionResult> VoteAnswerDescription(int answerDescriptionId = 0)
        {
            _logger.LogDebug("VoteAnswerDescription answerDescriptionId = " + answerDescriptionId);

            // Only do something is answer id is not zero
            if (answerDescriptionId != 0)
            {
                await _voteService.VoteAnswerDescription(
                    new AnswerDescriptionVoteDto() { AnswerDescriptionId = answerDescriptionId, UserId = _userManager.GetUserId(User) }
                    );
            }

            return RedirectToAction("ConfirmVote");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmVote(int answerId = 0)
        {
            return await Task.FromResult(View());
        }
    }
}
