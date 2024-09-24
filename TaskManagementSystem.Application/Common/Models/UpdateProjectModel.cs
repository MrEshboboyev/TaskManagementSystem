using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.Common.Models;

public class UpdateProjectModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
}

