using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.UI.ViewModels;

namespace TaskManagementSystem.UI.Controllers
{
    public class AccountController(ICompanyService companyService,
        IUserService userService) : Controller
    {
        private readonly ICompanyService _companyService = companyService;
        private readonly IUserService _userService = userService;


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
    }
}
