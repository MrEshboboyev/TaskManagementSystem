using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.Services.Interfaces;

namespace TaskManagementSystem.UI.Controllers
{
    [Authorize]
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


    }
}
