using BestFor.Domain.Entities;
using BestFor.Models;
using BestFor.Dto.Account;
using BestFor.Services.Messaging;
using BestFor.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Controllers
{
    /// <summary>
    /// Controller that handles user registration and logins.
    /// 
    /// This filter will parse the culture from the URL and set it into Viewbag.
    /// Controller has to inherit BaseApiController in order for filter to work correctly.
    /// </summary>
    [ServiceFilter(typeof(LanguageActionFilter))]
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private IProfanityService _profanityService;
        private readonly IResourcesService _resourcesService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender, ISmsSender smsSender, IUserService userService, IProfanityService profanityService,
            ILoggerFactory loggerFactory, IResourcesService resourcesService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _userService = userService;
            _profanityService = profanityService;
            _resourcesService = resourcesService;

        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var model = new LoginViewDto() { ReturnUrl = returnUrl };
            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                // User is cancelled -> Go to home page.
                if (user != null && user.IsCancelled)
                {
                    return RedirectToAction("Index", "Home");
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/AccessDenied
        /// <summary>
        /// Shown when user hit an unauthorized url
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // GET: /Account/SendCode
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewDto { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // redisplay the form if something is wrong with model.
            if (!ModelState.IsValid) return View(model);

            // Do profanity checks. We already validated the model.
            if (!await IsProfanityCleanProfileCreate(model)) return View(model);

            // Check email is unique.
            if (!await IsEmailUnique(model.Email, null)) return View(model);

            // Check display name is unique.
            if (!IsDisplayNameUnique(model.DisplayName, null)) return View(model);

            // Username will be checked by user manager.
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                DisplayName = model.DisplayName,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(3, "User created a new account with password.");

                // Add user to cache
                _userService.AddUserToCache(user);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null) // || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// GET: /Account/Profile
        /// Has to be logged in.
        /// Can only change email and display name. Display name can be blank or unique.
        /// Can not change user name.
        /// Can not change password.
        /// Have to type password to confirm update.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This is just a get to load current profile</remarks>
        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            var currentUserId = _userManager.GetUserId(User);

            // User manager will go to database to find info about the user.
            // Let's go to cache.
            // we are authenticated so currentUserId will be there.
            var user = _userService.FindById(currentUserId);

            // var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null)
            {
                // Would be funny if this happens. Someone is hacking us I guess.
                return View("Error");
            }
            var model = new ProfileViewDto();
            model.UserName = user.UserName;
            model.DisplayName = user.DisplayName;
            model.NumberOfAnswers = user.NumberOfAnswers;

            return View(model);
        }

        /// <summary>
        /// GET: /Account/Profile
        /// Has to be logged in.
        /// Can only change email and display name. Display name can be blank or unique.
        /// Can not change user name.
        /// Can not change password.
        /// Have to type password to confirm update.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This is just a get to load current profile</remarks>
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var currentUserId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null)
            {
                // Would be funny if this happens. Someone is hacking us I guess.
                return View("Error");
            }
            var model = new ProfileEditDto();
            model.Email = user.Email;
            model.UserName = user.UserName;
            model.DisplayName = user.DisplayName;

            return View(model);
        }

        /// <summary>
        /// POST: /Account/Profile
        /// Update user's profile. Does not navigate away.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ProfileEditDto model)
        {
            // Check the model first
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Find id
            var currentUserId = _userManager.GetUserId(User);
            // Find user
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null)
            {
                // Would be funny if this happens. Someone is hacking us I guess.
                return View("Error");
            }

            // First check if there was any changes
            if (model.DisplayName == user.DisplayName && user.Email == model.Email)
            {
                // No changes
                model.SuccessMessage = "Your profile was successfully updated.";
                return View(model);
            }

            // Do profanity checks. We already validated the model.
            if (!await IsProfanityCleanProfileUpdate(model)) return View(model);

            // Verify password
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid password");
                return View(model);
            }

            user.DisplayName = model.DisplayName;
            user.Email = model.Email;

            // Check email is unique.
            if (!await IsEmailUnique(model.Email, user.Id)) return View(model);

            // Check display name is unique.
            if (!IsDisplayNameUnique(model.DisplayName, user.Id)) return View(model);

            var updateResult = await _userManager.UpdateAsync(user);

            // If all good we stay on the same page and display success message.
            if (updateResult.Succeeded)
            {
                model.SuccessMessage = "Your profile was successfully updated.";
            }

            AddErrors(updateResult);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CancelProfile()
        {
            var currentUserId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null)
            {
                // Would be funny if this happens. Someone is hacking us I guess.
                return View("Error");
            }
            var model = new RemoveProfileViewModel();
            model.UserName = user.UserName;
            model.DisplayName = user.DisplayName;

            return View(model);
        }

        /// <summary>
        /// POST: /Account/Profile
        /// Update user's profile. Does not navigate away.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelProfile(RemoveProfileViewModel model)
        {
            // Check the model first
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Find id
            var currentUserId = _userManager.GetUserId(User);
            // Find user
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null)
            {
                // Would be funny if this happens. Someone is hacking us I guess.
                return View("Error");
            }

            user.IsCancelled = true;
            user.DateUpdated = DateTime.Now;
            user.DateCancelled = DateTime.Now;
            user.CancellationReason = model.Reason;

            var updateResult = await _userManager.UpdateAsync(user);

            // Log out.
            await _signInManager.SignOutAsync();

            // If all good we redirect to home.
            if (updateResult.Succeeded)
            {
                // Read the reason
                var reason = await _resourcesService.GetString(this.Culture, Lines.YOUR_PROFILE_WAS_REMOVED);
                return RedirectToAction("Index", "Home", new { reason = reason });
            }

            AddErrors(updateResult);

            return View(model);
        }

        #region Private Methods

        /// <summary>
        /// Put all errors from identity result into model state.
        /// </summary>
        /// <param name="result"></param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        //private async Task<ApplicationUser> GetCurrentUserAsync()
        //{
        //    return await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
        //}

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        /// <summary>
        /// Check profile update data for profanity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<bool> IsProfanityCleanProfileUpdate(ProfileEditDto model)
        {
            // Do profanity checks. We already validated the model.
            // we can only change a couple of fields.
            var result = await _profanityService.CheckProfanity(model.DisplayName);
            // Disaply name can be blank.
            if (!string.IsNullOrEmpty(model.DisplayName) && !string.IsNullOrWhiteSpace(model.DisplayName))
                if (result.HasIssues)
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);

            // We are not showing email anywhere so let's let users pick whatever the hell they want
            //result = _profanityService.CheckProfanity(model.Email);
            //if (result.HasIssues)
            //    ModelState.AddModelError(string.Empty, result.ErrorMessage);

            return ModelState.ErrorCount == 0;
        }

        /// <summary>
        /// Check profile creation data for profanity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<bool> IsProfanityCleanProfileCreate(RegisterViewModel model)
        {
            // Do profanity checks. We already validated the model.
            // we can only change a couple of fields.
            var result = await _profanityService.CheckProfanity(model.DisplayName);
            // Disaply name can be blank.
            if (!string.IsNullOrEmpty(model.DisplayName) && !string.IsNullOrWhiteSpace(model.DisplayName))
                if (result.HasIssues)
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);

            // We do want to check the username since it is displayable
            result = await _profanityService.CheckProfanity(model.UserName);
            if (result.HasIssues)
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

            // We are not showing email anywhere so let's let users pick whatever the hell they want
            //result = _profanityService.CheckProfanity(model.Email);
            //if (result.HasIssues)
            //    ModelState.AddModelError(string.Empty, result.ErrorMessage);

            return ModelState.ErrorCount == 0;
        }

        /// <summary>
        /// Check user's email uniqueness using user manager.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId">existing/current userid to skip</param>
        /// <returns></returns>
        private async Task<bool> IsEmailUnique(string email, string userId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (userId == user.Id)
                    return true; // ignore existing/current user

                ModelState.AddModelError(string.Empty, "This email is already taken.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check display name uniqueness using user service.
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="userId">existing userid to skip</param>
        /// <returns></returns>
        private bool IsDisplayNameUnique(string displayName, string userId)
        {
            // blank Display name is OK
            if (string.IsNullOrEmpty(displayName) || string.IsNullOrWhiteSpace(displayName)) return true;
            var user = _userService.FindByDisplayName(displayName);
            if (user != null)
            {
                if (userId == user.Id)
                    return true; // ignore existing/current user

                ModelState.AddModelError(string.Empty, "This display name is already taken.");
                return false;
            }
            return true;
        }
        #endregion
    }
}
