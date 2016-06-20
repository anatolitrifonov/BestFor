using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BestFor.Dto.Contact;
using BestFor.Domain.Entities;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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


        public ContactController(IResourcesService resourcesService, ILoggerFactory loggerFactory)
        {
            _resourcesService = resourcesService;
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactUsDto model)
        {

            model.UserName = _userManager.GetUserName(User);

            return await Task.FromResult<IActionResult>(View(model));
        }
    }
}
