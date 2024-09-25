using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Task;

public class TaskStatusDTO
{
    public string ManagerUserId { get; set; }
    public int ProjectId { get; set; }
    public TaskStatusEnum Status { get; set; }
}

