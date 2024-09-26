using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Notification;

public class NotificationCreateDTO
{
    public string SenderId { get; set; }
    public string RecipientId { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
}
