using System;
using BestFor.Services.Services;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BestFor.Controllers
{
    public class AdminController : Controller
    {

        private IStatusService _statusService;

        public AdminController(IStatusService statusService)
        {
            _statusService = statusService;
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
    }
}
