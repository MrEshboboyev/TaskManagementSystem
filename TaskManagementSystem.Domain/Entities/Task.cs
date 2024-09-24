using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities;

public class Task
{
    public int Id { get;  set; }                          // Unique identifier for the task
    public string Title { get;  set; }                     // Title of the task
    public string Description { get;  set; }               // Detailed description of the task
    public DateTime Deadline { get;  set; }                // Deadline by which the task should be completed
    public Enums.TaskStatus Status { get;  set; }                // Enum: Current status of the task (e.g., Todo, In Progress, Done)
    public TaskPriority Priority { get;  set; }            // Enum: Priority level of the task (e.g., Low, Medium, High)
    public string AssignedUserId { get;  set; }               // User assigned to the task
    public int ProjectId { get;  set; }                    // Associated project identifier
    public DateTime CreatedAt { get;  set; }               // Task creation timestamp
    public DateTime? UpdatedAt { get;  set; }              // Last update timestamp

    // Navigation Properties
    public ApplicationUser AssignedUser { get;  set; }                // Reference to the assigned user
    public Project Project { get;  set; }                  // Reference to the associated project
    public List<Comment> Comments { get; set; } 
}

