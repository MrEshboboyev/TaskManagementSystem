using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs;

public class UpdateTaskDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public TaskPriority Priority { get; set; }
}