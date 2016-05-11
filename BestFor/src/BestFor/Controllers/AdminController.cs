using System;
using BestFor.Services.Services;
using BestFor.Domain.Entities;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

namespace BestFor.Controllers
{
    /// <summary>
    /// Admin page controller. Nothing fancy yet. Gives ability to load data to cache and shows cache status.
    /// 
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private IStatusService _statusService;
        private IAnswerService _answerService;

        public AdminController(IStatusService statusService, IAnswerService answerService)
        {
            _statusService = statusService;
            _answerService = answerService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_statusService.GetSystemStatus());
        }

        // GET: /<controller>/LoadAnswers
        public IActionResult LoadAnswers()
        {
            _statusService.InitAnswers();
            return RedirectToAction("Index");
        }

        // GET: /<controller>/LoadSuggestions
        public IActionResult LoadSuggestions()
        {
            _statusService.InitSuggestions();
            return RedirectToAction("Index");
        }

        // GET: /<controller>/LoadBadWords
        public IActionResult LoadBadWords()
        {
            _statusService.InitBadWords();
            return RedirectToAction("Index");
        }

        // Edit Answer view
        public IActionResult EditAnswer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowAnswer(int answerId)
        {
            var answer = _answerService.FindById(answerId);

            return View(answer);
        }

        public IActionResult HideAnswer(int id)
        {
            var answer = _answerService.HideAnswer(id);

            return View(id);
        }
    }
}
