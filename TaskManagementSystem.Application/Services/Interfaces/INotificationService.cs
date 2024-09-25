using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Notification;

namespace TaskManagementSystem.Application.Services.Interfaces;

public interface INotificationService
{
    // Send a notification to a user
    Task<ResponseDTO<NotificationDTO>> SendNotificationAsync(NotificationCreateDTO notificationDto);

    // Mark a notification as read
    Task<ResponseDTO<bool>> MarkAsReadAsync(int notificationId);

    // Get all notifications for a specific user
    Task<ResponseDTO<IEnumerable<NotificationDTO>>> GetUserNotificationsAsync(string userId);

    // Get unread notifications for a user
    Task<ResponseDTO<IEnumerable<NotificationDTO>>> GetUnreadNotificationsAsync(string userId);

    // Delete a notification
    Task<ResponseDTO<bool>> DeleteNotificationAsync(int notificationId);
}