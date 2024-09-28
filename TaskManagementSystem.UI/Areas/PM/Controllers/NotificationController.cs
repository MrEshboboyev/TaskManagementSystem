using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.Common.Utility;
using TaskManagementSystem.Application.DTOs.Notification;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.UI.Areas.PM.ViewModels;

namespace TaskManagementSystem.UI.Areas.PM.Controllers
{
    [Area(SD.Role_PM)]
    [Authorize(Roles = SD.Role_PM)]
    public class NotificationController(INotificationService notificationService,
        ICompanyService companyService) : Controller
    {
        private readonly INotificationService _notificationService = notificationService;
        private readonly ICompanyService _companyService = companyService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("Login required!");
        #endregion

        public async Task<IActionResult> RequestsIndex()
        {
            var requestNotifications = await _notificationService.GetRequestNotificationsForUserAsync(GetUserId());
            return View(requestNotifications.Data);
        }
        
        public async Task<IActionResult> Details(int notificationId)
        {
            var requestNotification = await _notificationService.GetNotificationDetailsAsync(notificationId);
            return View(requestNotification.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int notificationId)
        {
            var notification = await _notificationService.GetNotificationDetailsAsync(notificationId);
            return View(notification.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int notificationId)
        {
            var result = await _notificationService.DeleteNotificationAsync(notificationId);

            if (result.Success) 
            {
                TempData["success"] = "Notification Successfully Deleted!";
                return RedirectToAction(nameof(RequestsIndex));
            }

            TempData["error"] = $"Failed to delete notification response. Error: {result.Message}";
            return RedirectToAction(nameof(Delete), new { notificationId });
        }

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            var companies = await _companyService.GetAllCompaniesAsync();

            NotificationCreateViewModel model = new()
            {
                Companies = companies.Data
            };

            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(NotificationCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            // Get the current PM's Email 
            var pmEmail = User.FindFirstValue(ClaimTypes.Email);

            // Fetch the company based on SelectedCompanyId
            var result = await _notificationService.SendNotificationToBossFromPMWithCompanyId(pmEmail, model.SelectedCompanyId);

            if (result.Success)
            {
                TempData["success"] = "Notification successfully sent!";
                return RedirectToAction(nameof(RequestsIndex));
            }

            TempData["error"] = $"Failed to sending notification process. Error : {result.Message}";
            return View(model);
        }
    }
}
