using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities;

public class Project
{
    public int Id { get; set; }                            // Unique identifier for the project
    public string Name { get; set; }                       // Name of the project
    public string Description { get;  set; }                // Detailed description of the project
    public DateTime StartDate { get;  set; }                // Date when the project started
    public DateTime EndDate { get;  set; }                  // Expected end date for the project
    public ProjectStatus Status { get;  set; }              // Enum: Current status of the project
    public string ManagerId { get;  set; }                 // User responsible for managing the project

    // Navigation Properties
    public ApplicationUser Manager { get;  set; }                      // Reference to the project manager
    public List<Task> Tasks { get;  set; }                  // List of tasks associated with the project
}

