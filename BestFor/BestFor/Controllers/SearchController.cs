﻿using Microsoft.AspNetCore.Identity;
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
        private readonly IAnswerService _answerService;

        public SearchController(IAnswerService answerService, UserManager<ApplicationUser> userManager, IResourcesService resourcesService,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _resourcesService = resourcesService;
            _answerService = answerService;
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

            var result = new AnswersDto();

            result.Answers = await _answerService.FindLeftAnswers(data);

            return View(result);
        }

        /// <summary>
        /// Request path is /first/{data} or /second/{data}
        /// Check that it does start with /first
        /// </summary>
        /// <param name="requestPath"></param>
        /// <returns></returns>
        /// <remarks>
        /// any funny url like /first/blah?blah#blah or /first/blah#blah?blah will be sut at the first blah
        /// </remarks>
        public string ParseData(string requestPath)
        {
            const string path = "/first/";
            if (requestPath == null) return null;
            if (!requestPath.ToLower().StartsWith(path)) return null;
            // cut /first
            var data = requestPath.Substring(path.Length);
            // see if there is a ? and cut from it too
            var question = data.IndexOf('?');
            if (question < 0) return data;
            var result = data.Substring(0, question);
            return result;
        }
    }
}