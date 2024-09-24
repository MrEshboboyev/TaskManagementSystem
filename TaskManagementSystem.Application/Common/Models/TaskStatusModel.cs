using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Common.Models;

public class TaskStatusModel
{
    public int ProjectId { get; set; }
    public TaskStatusEnum Status { get; set; }
}

