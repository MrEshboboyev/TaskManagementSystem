using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.Common.Utility;
using TaskManagementSystem.Application.Services.Interfaces;

namespace TaskManagementSystem.UI.Areas.PM.Controllers
{
    [Area(SD.Role_PM)]
    [Authorize(Roles = SD.Role_PM)]
    public class NotificationController(INotificationService notificationService) : Controller
    {
        private readonly INotificationService _notificationService = notificationService;

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
    }
}
