namespace TaskManagementSystem.Domain.Entities;

public class Comment
{
    public int Id { get;  set; }                            // Unique identifier for the comment
    public int TaskId { get;  set; }                        // The task the comment is associated with
    public string UserId { get;  set; }                        // The user who made the comment
    public string Content { get;  set; }                    // The comment text
    public DateTime CreatedAt { get;  set; }                // Timestamp for when the comment was created

    // Navigation Properties
    public TaskItem Task { get;  set; }                         // Reference to the associated task
    public ApplicationUser User { get;  set; }                         // Reference to the user who created the comment
}
