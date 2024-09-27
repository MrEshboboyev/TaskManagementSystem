using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.Common.Utility;
using TaskManagementSystem.Application.DTOs.Notification;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.UI.Areas.Boss.Controllers
{
    [Area(SD.Role_Boss)]
    [Authorize(Roles = SD.Role_Boss)]
    public class NotificationController(INotificationService notificationService) : Controller
    {
        private readonly INotificationService _notificationService = notificationService;

        #region Private Methods
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("Login required!");
        #endregion


        public async Task<IActionResult> Index()
        {
            var userNotifications = await _notificationService.GetNotificationsForUserAsync(GetUserId());
            return View(userNotifications.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int notificationId)
        {
            var notification = await _notificationService.GetNotificationDetailsAsync(notificationId);
            return View(notification.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int notificationId, NotificationStatus status)
        {
            NotificationResponseDTO notificationResponseDTO = new() { Status = status };

            var result = await _notificationService.RespondToNotificationAsync(notificationId,
                notificationResponseDTO);

            if (result.Success)
            {
                TempData["success"] = "Notification response has been successfully updated.";
                return RedirectToAction("Index");
            }

            TempData["error"] = $"Failed to update notification response. Error: {result.Message}";
            return RedirectToAction(nameof(Update), new { notificationId });
        }
    }
}
