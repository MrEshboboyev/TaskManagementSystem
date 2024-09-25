using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Project;

public class ProjectDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public string ManagerName { get; set; }   // Manager's name
}

