using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Task;

public class TaskDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public TaskPriority Priority { get; set; }
    public string AssignedUser { get; set; } // User's full name or email
    public TaskStatusEnum Status { get; set; }
    public string ProjectName { get; set; }
}

