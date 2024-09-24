using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Services.Interfaces;

public interface INotificationService
{
    // Send a notification to a user
    Task<NotificationDTO> SendNotificationAsync(NotificationCreateDTO notificationDto);

    // Mark a notification as read
    Task MarkAsReadAsync(int notificationId);

    // Get all notifications for a specific user
    Task<IEnumerable<NotificationDTO>> GetUserNotificationsAsync(string userId);

    // Get unread notifications for a user
    Task<IEnumerable<NotificationDTO>> GetUnreadNotificationsAsync(string userId);

    // Delete a notification
    Task DeleteNotificationAsync(int notificationId);
}