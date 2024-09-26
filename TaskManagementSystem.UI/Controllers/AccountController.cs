using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.UI.ViewModels;
using TaskManagementSystem.UI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Application.DTOs.Notification;

namespace TaskManagementSystem.UI.Controllers
{
    public class AccountController(ICompanyService companyService,
        IUserService userService,
        IAuthService authService,
        ITokenProvider tokenProvider,
        SignInManager<ApplicationUser> signInManager,
        INotificationService notificationService) : Controller
    {
        private readonly ICompanyService _companyService = companyService;
        private readonly IUserService _userService = userService;
        private readonly IAuthService _authService = authService;
        private readonly ITokenProvider _tokenProvider = tokenProvider;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly INotificationService _notificationService = notificationService;

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var allCompanies = (await _companyService.GetAllCompaniesAsync()).Data;
            var projectManagers = (await _userService.GetPMs()).Data;

            RegisterViewModel registerViewModel = new()
            {
                Companies = allCompanies,
                PMs = projectManagers
            };

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            RegisterModel registerModel = new()
            {
                Email = model.Email,
                FullName = model.FullName,
                Password = model.Password,
                CompanyId = model.SelectedCompanyId,
                PMId = model.SelectedPMId,
                RoleName = model.Role
            };

            var result = await _authService.RegisterAsync(registerModel);

            if (result.Success)
            {
                TempData["success"] = result.Message;
                await _notificationService.SendNotificationToBossFromPMWithCompanyId(model.Email, model.SelectedCompanyId);
                return RedirectToAction(nameof(Login));
            }

            var allCompanies = (await _companyService.GetAllCompaniesAsync()).Data;
            var projectManagers = (await _userService.GetPMs()).Data;

            // assign
            model.Companies = allCompanies;
            model.PMs = projectManagers;

            TempData["error"] = result.Message;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _authService.LoginAsync(loginModel);

            if (result.Success)
            {
                // sign in user applied
                await SignInUser(result.Data);

                // set token for user
                _tokenProvider.SetToken(result.Data);

                TempData["success"] = "Login successfully!";
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = result.Message;

            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetProjectManagers(int companyId)
        {
            var projectManagers = await _userService.GetProjectManagersByCompanyId(companyId);

            return Json(projectManagers.Data);
        }


        #region Private Methods
        // Sign In User
        private async Task SignInUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            // adding claims
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        #endregion
    }
}
