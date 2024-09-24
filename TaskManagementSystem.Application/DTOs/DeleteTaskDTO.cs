namespace TaskManagementSystem.Application.DTOs;

public class DeleteTaskDTO
{
    public string ManagerUserId { get; set; }
    public int ProjectId { get; set; }
    public int TaskId { get; set; }
}
