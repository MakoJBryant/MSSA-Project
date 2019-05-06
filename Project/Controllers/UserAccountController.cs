using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WebUI.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebUI.Controllers
{

    public class UserAccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserAccountViewModel> _logger;
        private readonly Dictionary<int, string> _topics = new Dictionary<int, string>();

        // Access the data and the user manager with dependency injection
        public UserAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UserAccountViewModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // Get the current users ID
        private string _currentUser { get { return _userManager.GetUserId(User); } }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            UserAccountViewModel UserModel = new UserAccountViewModel
            {

            };

            return View(UserModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            UserAccountViewModel Avm = new UserAccountViewModel();

            return View(Avm);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([Bind("Email, Password")] UserAccountViewModel Input)
        {
            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _signInManager.PasswordSignInAsync(user, Input.Password, isPersistent: false, lockoutOnFailure: true);
                return LocalRedirect("/Home/Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // If we got this far, something failed, redisplay form
            return PartialView("_RegisterForm");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            UserAccountViewModel model = new UserAccountViewModel();

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("Email,Password,RememberMe")]UserAccountViewModel viewModel)
        {
            string returnUrl = Url.Content("~/");
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect("/Home/Index");
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = viewModel.RememberMe });
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View("_AccountError");
            }
        }       
    }
}