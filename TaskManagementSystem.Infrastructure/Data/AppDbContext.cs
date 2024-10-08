﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskManagementSystem.Domain.Entities;
using TaskItem = TaskManagementSystem.Domain.Entities.TaskItem;

namespace TaskManagementSystem.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :
        IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TaskItem>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);  // Optional, define how deletions should behave

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
            builder.Entity<TaskItem>()
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

            // Configure Notification relationships
            builder.Entity<Notification>()
                .HasOne(n => n.Sender)
                .WithMany()  // Sender doesn't need a navigation property for the notifications they send
                .HasForeignKey(n => n.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete to prevent issues when deleting a user

            builder.Entity<Notification>()
                .HasOne(n => n.Recipient)
                .WithMany(u => u.Notifications)  // Recipient has a navigation property for received notifications
                .HasForeignKey(n => n.RecipientId)
                .OnDelete(DeleteBehavior.Cascade); // Notifications should be deleted when the recipient is deleted
            
            // Company and ApplicationUser (Owner relationship)
            builder.Entity<Company>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.CompaniesOwned)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete if the user is deleted
        }
    }
}
