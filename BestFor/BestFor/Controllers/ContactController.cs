using BestFor.Domain.Entities;
using BestFor.Dto.Contact;
using BestFor.Services.Messaging;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BestFor.Controllers
{
    /// <summary>
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// 
    /// Implements the contact us screens.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    public class ContactController : BaseApiController
    {
        /// <summary>
        /// Constructor injected properties.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResourcesService _resourcesService;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;


        public ContactController(UserManager<ApplicationUser> userManager, IResourcesService resourcesService, IEmailSender emailSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _resourcesService = resourcesService;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.LogInformation("created ContactController");
        }

        /// <summary>
        /// Default contact us page view.
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var model = new ContactUsDto();

            model.UserName = _userManager.GetUserName(User);
            if (model.UserName == null) model.UserName = "Anonymous";

            return await Task.FromResult<IActionResult>(View(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactUsDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.UserName = _userManager.GetUserName(User);

            string message = "User " + model.UserName + " is contacting us. " + model.Content;

            await _emailSender.SendEmailAsync(model.Subject, message);

            // Read the reason
            var reason = await _resourcesService.GetString(this.Culture, Lines.THANK_YOU_FOR_CONTACTING);

            // return await Task.FromResult<IActionResult>(View(model));
            return RedirectToAction("Index", "Home", new { reason = reason });

        }
    }
}
