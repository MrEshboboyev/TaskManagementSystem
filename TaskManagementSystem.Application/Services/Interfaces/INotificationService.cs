using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Notification;

namespace TaskManagementSystem.Application.Services.Interfaces;

public interface INotificationService
{
    // Send a notification to a user
    Task<ResponseDTO<NotificationDTO>> CreateNotificationAsync(NotificationCreateDTO notificationDto);

    // Mark a notification as read
    Task<ResponseDTO<bool>> MarkAsReadAsync(int notificationId);

    // Marks all notifications as read
    Task<ResponseDTO<bool>> MarkAllAsReadAsync(string userId);

    // Get all notifications for a specific user
    Task<ResponseDTO<IEnumerable<NotificationDTO>>> GetNotificationsForUserAsync(string userId);

    // Get pending notifications for a user
    Task<ResponseDTO<IEnumerable<NotificationDTO>>> GetPendingNotificationsAsync(string userId);

    // Delete a notification
    Task<ResponseDTO<bool>> DeleteNotificationAsync(int notificationId);

    Task<ResponseDTO<bool>> RespondToNotificationAsync(int notificationId, NotificationResponseDTO responseDTO);

    Task<ResponseDTO<bool>> NotifyPMStatusChangeAsync(string pmId, bool isAccepted);

    Task<ResponseDTO<NotificationDTO>> GetNotificationDetailsAsync(int notificationId);
}