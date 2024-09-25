namespace TaskManagementSystem.Application.DTOs.Task;

public class TaskDeleteDTO
{
    public string ManagerUserId { get; set; }
    public int ProjectId { get; set; }
    public int TaskId { get; set; }
}
