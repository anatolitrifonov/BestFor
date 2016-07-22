using BestFor.Dto;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BestFor.ViewComponents
{
    public class TrendingOverallOpinionViewComponent : ViewComponent
    {
        /// <summary>
        /// Constructor injected answer service. Used for loading the answers.
        /// </summary>
        private IAnswerService _answerService;
        private ILogger _logger;

        public TrendingOverallOpinionViewComponent(IAnswerService answerService, ILoggerFactory loggerFactory)
        {
            _answerService = answerService;
            _logger = loggerFactory.CreateLogger<TrendingOverallOpinionViewComponent>();
            _logger.LogInformation("created TrendingOverallOpinionViewComponent");
        }

        /// <summary>
        /// This is the main rendering method
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = new AnswersDto();
            data.Answers = await _answerService.FindAnswersTrendingOverall();

            return View(data);
        }
    }
}
