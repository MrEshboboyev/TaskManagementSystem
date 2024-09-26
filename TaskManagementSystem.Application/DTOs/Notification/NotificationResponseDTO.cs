using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Notification
{
    public class NotificationResponseDTO
    {
        public NotificationStatus Status { get; set; }  // Accepted or Declined
    }
}
