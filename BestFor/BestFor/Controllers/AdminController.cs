using BestFor.Services.Services;
using BestFor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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
        private IAnswerDescriptionService _answerDescriptionService;
        private IUserService _userService;

        public AdminController(IStatusService statusService, IAnswerService answerService, IAnswerDescriptionService answerDescriptionService, IUserService userService)
        {
            _statusService = statusService;
            _userService = userService;
            _answerService = answerService;
            _answerDescriptionService = answerDescriptionService;
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

        public IActionResult LoadAnswerDescriptions()
        {
            _statusService.InitAnswerDescriptions();
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

        // GET: /<controller>/LoadUsers
        public IActionResult LoadUsers()
        {
            _statusService.InitUsers();
            return RedirectToAction("Index");
        }

        // Edit Answer view
        public IActionResult EditAnswer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShowAnswer(int answerId)
        {
            var model = await FillInTheAnswer(answerId);

            return View(model);
        }

        public async Task<IActionResult> GetAnswer(int answerId)
        {
            var model = await FillInTheAnswer(answerId);

            return View("ShowAnswer", model);
        }

        public async Task<AdminAnswerViewModel> FillInTheAnswer(int answerId)
        {
            var answer = await _answerService.FindByAnswerId(answerId);
            // Load descriptions directly from database
            var descriptions = await _answerDescriptionService.FindDirectByAnswerId(answerId);

            var model = new AdminAnswerViewModel() { Answer = answer, AnswerDescriptions = descriptions };
            return model;
        }

        public async Task<IActionResult> HideAnswer(int id)
        {
            var answer = await _answerService.HideAnswer(id);

            return View(id);
        }

        public IActionResult ListUser()
        {
            var users = _userService.FindAll();

            return View(users.OrderBy(x => x.DisplayName).ThenBy(x => x.UserName));
        }

        public async Task<IActionResult> ShowUser(string id)
        {
            var user = _userService.FindById(id);

            var answers = await _answerService.FindDirectByUserId(id);

            var model = new AdminUserViewModel() { User = user, Answers = answers };

            return View(model);
        }

        /// <summary>
        /// Show all answer descriptions that user added.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ShowUserDescriptions(string id)
        {
            var user = _userService.FindById(id);

            var answerDescriptions = await _answerDescriptionService.FindDirectByUserId(id);

            var model = new AdminUserDescriptionsViewModel() { User = user, AnswerDescriptions = answerDescriptions };

            return View(model);
        }

        public async Task<IActionResult> ListBlankAnswer()
        {
            var answers = await _answerService.FindDirectBlank();

            return View(answers);
        }

        public async Task<IActionResult> ListBlankDescription()
        {
            var answers = await _answerDescriptionService.FindDirectBlank();

            return View(answers);
        }
    }
}
