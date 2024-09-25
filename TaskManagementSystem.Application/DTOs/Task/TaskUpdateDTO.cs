using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Task;

public class TaskUpdateDTO
{
    public string ManagerUserId { get; set; }
    public int ProjectId { get; set; }
    public string AssignedUserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public TaskPriority Priority { get; set; }
}