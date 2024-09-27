using TaskManagementSystem.Application.DTOs.Company;

namespace TaskManagementSystem.UI.Areas.PM.ViewModels;

public class NotificationCreateViewModel
{
    public IEnumerable<CompanyDTO>? Companies { get; set; }
    public int SelectedCompanyId { get; set; }
}
