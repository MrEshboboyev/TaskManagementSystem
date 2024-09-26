using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities;

public class Notification
{
    public int Id { get; set; }                        // Unique identifier for the notification
    public string SenderId { get; set; }               // User who sent the notification (could be system or another user)
    public string RecipientId { get; set; }            // User who will receive the notification
    public string Message { get; set; }                // Notification message content
    public NotificationType Type { get; set; }         // Type of notification (e.g., PM registration, approval result)
    public NotificationStatus Status { get; set; }     // Status (e.g., Pending, Accepted, Declined)
    public DateTime CreatedAt { get; set; }            // Timestamp for when the notification was created
    public DateTime? ActionTakenAt { get; set; }       // Timestamp for when any action was taken (optional)
    public bool IsRead { get; set; }                   // Whether the notification has been read or not

    // Navigation properties
    public ApplicationUser Sender { get; set; }        // Reference to the sender of the notification
    public ApplicationUser Recipient { get; set; }     // Reference to the recipient of the notification
}
