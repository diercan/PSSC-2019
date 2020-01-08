using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrBaschet.Domain.Entities;
using FrBaschet.Domain.ViewModels.AccountViewModels;
using FrBaschet.Infrastructure.Data;
using FrBaschet.Infrastructure.Data.Context;
using FrBaschet.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrBaschet.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FrBaschetContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger, IEmailSender emailSender, FrBaschetContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;

            _logger.LogInformation("ctx");
        }

        [HttpGet]
        [Route("account/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated) return LocalRedirect(Url.Action("Index", "Home"));

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewBag.Errors = new List<string>();
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return LocalRedirect(Url.Action("Index", "Home"));
            }

            return View();
        }

        [HttpGet]
        [Route("account/logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            foreach (var cookieKey in Request.Cookies.Keys) Response.Cookies.Delete(cookieKey);

            return View();
        }

        [HttpGet]
        [Route("account/forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        [Route("account/register")]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null, [FromQuery] string code = "")
        {
            if (User.Identity.IsAuthenticated) return LocalRedirect(Url.Action("Index", "Home"));

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("account/register")]
        public async Task<IActionResult> OnPostAsync(RegisterViewModel registerViewModel,
            [FromQuery] string code = "")
        {
            var userCode = _context.InvitationEntityModels.FirstOrDefault(t => t.Code == code);
            if (userCode == null) return BadRequest("Code invalid");

            if (!ModelState.IsValid) return BadRequest();

            var appUser = RoleHelper.CreateUser(userCode.Role);
            appUser.FirstName = registerViewModel.FirstName;
            appUser.LastName = registerViewModel.LastName;
            appUser.Email = userCode.Email;
            appUser.PhoneNumber = registerViewModel.Phone;
            appUser.UserName = registerViewModel.Username;
            var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(appUser, RoleHelper.GetRole(userCode.Role));

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    return RedirectToPage("RegisterConfirmation",
                        new {email = registerViewModel.Email});

                await _signInManager.SignInAsync(appUser, false);
                return LocalRedirect(Url.Action("Index", "Home"));
            }

            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);

            return BadRequest(ModelState);
        }
    }
}