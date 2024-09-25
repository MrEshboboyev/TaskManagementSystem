using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.DTOs.Notification;
using TaskManagementSystem.Application.Services.Interfaces;

namespace TaskManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(INotificationService notificationService) : ControllerBase
    {
        private readonly INotificationService _notificationService = notificationService;

        #region Private Methods

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new Exception("Login required!");
        #endregion

        [HttpGet("get-user-notifications")]
        public async Task<IActionResult> GetUserNotifications()
        {
            var result = await _notificationService.GetUserNotificationsAsync(GetUserId());

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("get-unread-notification")]
        public async Task<IActionResult> GetUnreadNotifications()
        {
            var result = await _notificationService.GetUnreadNotificationsAsync(GetUserId());

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            var result = await _notificationService.MarkAsReadAsync(notificationId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("send-notification")]
        public async Task<IActionResult> SendNotification(string message)
        {
            NotificationCreateDTO notificationCreateDTO = new()
            {
                Message = message,
                UserId = GetUserId()
            };

            var result = await _notificationService.SendNotificationAsync(notificationCreateDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("delete-notification")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            var result = await _notificationService.DeleteNotificationAsync(notificationId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
