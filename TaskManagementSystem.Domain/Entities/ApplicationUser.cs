using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
