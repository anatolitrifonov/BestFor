using BestFor.Domain.Entities;
using BestFor.Dto;
using BestFor.Services.Services;
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

        /// <summary>
        /// Search on the left word
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Left()
        {
            _logger.LogDebug("SearchController Left");

            // we expect /{culture}/left/{data} or /left/{data} this will give us /left/{data}
            var requestPath = this.RequestPathNoCulture;

            var data = ParseData(requestPath, "left");

            var result = new AnswersDto();

            result.Answers = await _answerService.FindLeftAnswers(data);
            result.SearchKeyword = data;

            return View("Result", result);
        }

        /// <summary>
        /// Search on the right word
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Right()
        {
            _logger.LogDebug("SearchController Right");

            // we expect /{culture}/right/{data} or /right/{data} this will give us /right/{data}
            var requestPath = this.RequestPathNoCulture;

            var data = ParseData(requestPath, "right");

            var result = new AnswersDto();

            result.Answers = await _answerService.FindRightAnswers(data);
            result.IsLeft = false;
            result.SearchKeyword = data;

            return View("Result", result);
        }

        /// <summary>
        /// Return all data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Everything()
        {
            _logger.LogDebug("SearchController Everything");

            var result = new AnswersDto();
            result.Answers = await _answerService.FindLastAnswers();

            return View(result);
        }

        /// <summary>
        /// Return all data
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Everything(string searchPhrase)
        {
            _logger.LogDebug("SearchController Everything(" + searchPhrase + ")");

            var result = new AnswersDto();
            result.Answers = await _answerService.FindLastAnswers(searchPhrase);

            return View(result);
        }


        /// <summary>
        /// Request path is /left/{data} or /right/{data}
        /// Check that it does start with /first
        /// </summary>
        /// <param name="requestPath"></param>
        /// <returns></returns>
        /// <remarks>
        /// any funny url like /first/blah?blah#blah or /first/blah#blah?blah will be cut at the first blah
        /// </remarks>
        public string ParseData(string requestPath, string matchPath)
        {
            string path = "/" + matchPath + "/";
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
