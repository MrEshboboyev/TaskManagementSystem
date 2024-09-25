using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Project;

public class ProjectTaskDTO
{
    public int TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public TaskStatusEnum Status { get; set; }
}

