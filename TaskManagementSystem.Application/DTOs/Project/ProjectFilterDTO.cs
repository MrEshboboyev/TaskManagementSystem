using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Project;

public class ProjectFilterDTO
{
    public ProjectStatus? Status { get; set; }
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
}
