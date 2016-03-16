using BestFor.Dto;
using BestFor.Services;
using Microsoft.AspNet.Mvc;
using BestFor.Services.Services;
using Microsoft.Extensions.Logging;

namespace BestFor.Controllers
{
    /// <summary>
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    public class HomeController : BaseApiController
    {
        /// <summary>
        /// Constructor injected answer service. Used for loading the answers.
        /// </summary>
        private IAnswerService _answerService;
        private IResourcesService _resourcesService;
        private ILogger _logger;

        public HomeController(IAnswerService answerService, IResourcesService resourcesService, ILoggerFactory loggerFactory)
        {
            _answerService = answerService;
            _resourcesService = resourcesService;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.LogInformation("created HomeController");
        }

        /// <summary>
        /// Default home page view.
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = new HomePageDto();

            model.TopToday.Answers = _answerService.FindAnswersTrendingToday();

            model.TopOverall.Answers = _answerService.FindAnswersTrendingOverall();

            model.Culture = this.Culture;

            return View(model);
        }

        /// <summary>
        /// This view will be rendered if answer is in the URL string.
        /// </summary>
        /// <returns></returns>
        public IActionResult MyContent()
        {
            var culture = this.Culture;
            var requestPath = Request.Path.Value;
            // cut the culture
            var cultureBegining = "/" + culture;
            if (requestPath.StartsWith(cultureBegining)) requestPath = requestPath.Substring(cultureBegining.Length);
            // Now try to parse the request path into known words.
            var commonStrings = _resourcesService.GetCommonStrings(culture);
            var answer = LinkingHelper.ParseUrlToAnswer(commonStrings, requestPath);
            // Were we able to parse?
            if (answer == null) RedirectToAction("Index");
            // Let's try to find that answer
            answer = _answerService.FindExact(answer.LeftWord, answer.RightWord, answer.Phrase);
            // Go to home index if not found
            if (answer == null) RedirectToAction("Index");
            // Fill in result
            var data = new MyContentDto() { Answer = answer, CommonStrings = commonStrings };
            return View(data);
        }
    }
}
