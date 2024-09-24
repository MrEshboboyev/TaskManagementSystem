namespace TaskManagementSystem.Domain.Entities;

public class Notification
{
    public int Id { get;  set; }                            // Unique identifier for the notification
    public string UserId { get;  set; }                        // The user who will receive the notification
    public string Message { get;  set; }                    // Notification message content
    public DateTime CreatedAt { get;  set; }                // Timestamp for when the notification was created
    public bool IsRead { get;  set; }                       // Whether the notification has been read or not

    // Navigation Properties
    public ApplicationUser User { get;  set; }                         // Reference to the user who will receive the notification
}
