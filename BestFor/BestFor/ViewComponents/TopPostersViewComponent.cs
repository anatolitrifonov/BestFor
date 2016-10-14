using BestFor.Dto.Account;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

namespace BestFor.ViewComponents
{
    /// <summary>
    /// Shows all top posters.
    /// </summary>
    public class TopPostersViewComponent : ViewComponent
    {
        /// <summary>
        /// Constructor injected answer service. Used for loading the answers.
        /// </summary>
        private IAnswerService _answerService;
        private ILogger _logger;

        public TopPostersViewComponent(IAnswerService answerService, ILoggerFactory loggerFactory)
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
            var data = new ApplicationUsersDto();
            data.Users = await _answerService.FindTopPosterIds();
            return View(data);
        }
    }
}
