using BestFor.Dto;
using Microsoft.AspNet.Mvc;
using BestFor.Services.Services;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BestFor.Controllers
{
  //  [ServiceFilter(typeof(LanguageActionFilter))]
    // [Route("{culture}/[controller]")]
    public class HomeController : Controller
    {
        private IAnswerService _answerService;

        public HomeController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = new HomePageDto();

            model.TopToday.Answers = _answerService.FindAnswersTrendingToday();

            model.TopOverall.Answers = _answerService.FindAnswersTrendingOverall();

            return View(model);
        }

        public IActionResult MyContent()
        {
            var s = Request.Path;



            var data = new MyContentDto();

            return View(data);
        }
    }
}
