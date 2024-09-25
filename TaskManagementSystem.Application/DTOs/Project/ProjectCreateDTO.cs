namespace TaskManagementSystem.Application.DTOs.Project;

public class ProjectCreateDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ManagerUserId { get; set; }
}
