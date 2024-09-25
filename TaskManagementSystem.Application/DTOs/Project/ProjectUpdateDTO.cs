using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Project;

public class ProjectUpdateDTO
{
    public string ManagerUserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
}

