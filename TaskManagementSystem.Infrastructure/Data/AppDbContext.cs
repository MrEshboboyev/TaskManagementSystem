using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities;
using Task = TaskManagementSystem.Domain.Entities.Task;

namespace TaskManagementSystem.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : 
        IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 1. ApplicationUser to Task (One-to-Many relationship)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.AssignedTasks)                     // One user has many assigned tasks
                .WithOne()                                         // A task is assigned to one user
                .HasForeignKey(t => t.AssignedUserId)              // Foreign Key in Task
                .OnDelete(DeleteBehavior.Restrict);                // Optional: Prevent cascading deletes

            // 2. ApplicationUser to Project (One-to-Many relationship)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.ManagedProjects)                   // One user can manage many projects
                .WithOne(p => p.Manager)                           // A project is managed by one user
                .HasForeignKey(p => p.ManagerUserId)               // Foreign Key in Project
                .OnDelete(DeleteBehavior.Restrict);                // Optional: Prevent cascading deletes

            // 3. Project to Task (One-to-Many relationship)
            builder.Entity<Project>()
                .HasMany(p => p.Tasks)                             // A project has many tasks
                .WithOne(t => t.Project)                           // A task belongs to one project
                .HasForeignKey(t => t.ProjectId)                   // Foreign Key in Task
                .OnDelete(DeleteBehavior.Cascade);                 // Optional: Cascade delete if project is deleted

            // 4. Task to Comment (One-to-Many relationship)
            builder.Entity<Task>()
                .HasMany(t => t.Comments)                          // A task can have many comments
                .WithOne(c => c.Task)                              // A comment belongs to one task
                .HasForeignKey(c => c.TaskId)                      // Foreign Key in Comment
                .OnDelete(DeleteBehavior.Cascade);                 // Optional: Cascade delete if task is deleted

            // 5. ApplicationUser to Comment (One-to-Many relationship)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Comments)                          // One user can post many comments
                .WithOne(c => c.User)                              // A comment is posted by one user
                .HasForeignKey(c => c.UserId)                      // Foreign Key in Comment
                .OnDelete(DeleteBehavior.Cascade);                 // Optional: Cascade delete if user is deleted

            // 6. ApplicationUser to Notification (One-to-Many relationship)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Notifications)                     // One user can receive many notifications
                .WithOne(n => n.User)                              // A notification is tied to one user
                .HasForeignKey(n => n.UserId)                      // Foreign Key in Notification
                .OnDelete(DeleteBehavior.Cascade);                 // Optional: Cascade delete if user is deleted
        }
    }
}
