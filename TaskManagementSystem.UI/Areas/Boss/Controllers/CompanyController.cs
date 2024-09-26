﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Security.Claims;
using TaskManagementSystem.Application.Common.Utility;
using TaskManagementSystem.Application.DTOs.Company;
using TaskManagementSystem.Application.Services.Interfaces;

namespace TaskManagementSystem.UI.Areas.Boss.Controllers
{
    [Area("Boss")]
    [Authorize(Roles = SD.Role_Boss)]
    public class CompanyController(ICompanyService companyService) : Controller
    {
        private readonly ICompanyService _companyService = companyService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new Exception("Login required!");
        #endregion

        [HttpGet]
        public async Task<IActionResult> CompanyIndex()
        {
            var allCompanies = await _companyService.GetUserCompaniesAsync(GetUserId());
            return View(allCompanies.Data);
        }

        [HttpGet]
        public IActionResult CompanyCreate() => View();

        [HttpPost]
        public async Task<IActionResult> CompanyCreate(CompanyCreateDTO companyCreateDTO)
        {
            var result = await _companyService.CreateCompany(companyCreateDTO);

            if (!result.Success)
            {
                TempData["error"] = result.Message;
                return View(companyCreateDTO);
            }

            TempData["success"] = "Company successfully created";
            return RedirectToAction(nameof(CompanyIndex));
        }

        [HttpGet]
        public async Task<IActionResult> CompanyUpdate(int companyId)
        {
            var company = await _companyService.GetCompanyAsync(companyId);

            return View(company.Data);
        }

        [HttpGet]
        public async Task<IActionResult> CompanyDelete(int companyId)
        {
            var company = await _companyService.GetCompanyAsync(companyId);

            return View(company.Data);
        }
    }
}
