using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BestFor.Controllers
{
    public class TestsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SuggestionLineItem()
        {
            return View();
        }

        public IActionResult SuggestionResultList()
        {
            return View();
        }

        public IActionResult SuggestionTextBox()
        {
            return View();
        }

        public IActionResult SuggestionControl()
        {
            return View();
        }

        public IActionResult SuggestionAnswerList()
        {
            return View();
        }
    }
}
