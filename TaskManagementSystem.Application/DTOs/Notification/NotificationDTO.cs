using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Notification;

public class NotificationDTO
{
    public int Id { get; set; }
    public string SenderName { get; set; }
    public string RecipientName { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public NotificationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
}
