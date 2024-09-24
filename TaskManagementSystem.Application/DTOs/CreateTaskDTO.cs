using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs;

public class CreateTaskDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
    public string AssignedUserId { get; set; }
}