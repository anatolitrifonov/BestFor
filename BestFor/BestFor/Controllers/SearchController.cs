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
    public class SearchController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IResourcesService _resourcesService;

        public SearchController(UserManager<ApplicationUser> userManager, IVoteService voteService, IResourcesService resourcesService,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _resourcesService = resourcesService;
            _logger = loggerFactory.CreateLogger<SearchController>();
            _logger.LogInformation("created SearchController");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug("SearchController Get");

            // we expect /{culture}/search/{data} or /search/{data} this will give us /search/{data}
            var requestPath = this.RequestPathNoCulture;

            var data = ParseData(requestPath);


            return View();

            //// Only do something is answer id is not zero
            //if (answerId != 0)
            //{
            //    await _voteService.VoteAnswer(new AnswerVoteDto() { AnswerId = answerId, UserId = _userManager.GetUserId(User) } );
            //}

            //// Read the reason
            //var reason = await _resourcesService.GetString(this.Culture, Lines.THANK_YOU_FOR_VOTING);

            //return RedirectToAction("ShowAnswer", "AnswerAction", new { answerId = answerId, reason = reason });
        }

        public string ParseData(string requestPath)
        {
            if (requestPath == null) return null;
            if (!requestPath.ToLower().StartsWith("/first")) return null;
            // cut /first
            var data = requestPath.Substring("/first".Length);
            // see if there is a ? and cut from it too
            var question = data.IndexOf('?');
            if (question < 0) return data;
            var result = data.Substring(0, question);
            return result;
        }
    }
}
