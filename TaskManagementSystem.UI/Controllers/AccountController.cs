using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.UI.ViewModels;

namespace TaskManagementSystem.UI.Controllers
{
    public class AccountController(ICompanyService companyService,
        IUserService userService,
        IAuthService authService) : Controller
    {
        private readonly ICompanyService _companyService = companyService;
        private readonly IUserService _userService = userService;
        private readonly IAuthService _authService = authService;


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
                return RedirectToAction("Index", "Home");
            }

            var allCompanies = (await _companyService.GetAllCompaniesAsync()).Data;
            var projectManagers = (await _userService.GetPMs()).Data;

            // assign
            model.Companies = allCompanies;
            model.PMs = projectManagers;

            TempData["error"] = result.Message;
            return View(model);
        }
    }
}
