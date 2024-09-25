using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Application.DTOs.Company;
using TaskManagementSystem.Application.DTOs.User;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UI.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password must be match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; } // 'Boss', 'PM', 'Employee'


        // List of companies, PMs (for employee)
        public IEnumerable<CompanyDTO>? Companies { get; set; }
        public IEnumerable<UserDTO>? PMs { get; set; }

        // selected companyId and PMid
        public int? SelectedCompanyId { get; set; }
        public int? SelectedPMId { get; set; }
    }
}
