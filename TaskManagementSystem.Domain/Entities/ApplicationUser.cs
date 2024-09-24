using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        // Navigation Properties
        public List<Task> AssignedTasks { get;  set; }          // List of tasks assigned to this user
        public List<Project> ManagedProjects { get;  set; }     // List of projects managed by this user
        public List<Comment> Comments { get;  set; }     // List of comments by this user
        public List<Notification> Notifications { get;  set; }     // List of notifications 
    }
}
