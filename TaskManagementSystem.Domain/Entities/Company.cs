namespace TaskManagementSystem.Domain.Entities;

public class Company
{
    public int Id { get; set; }                    // Unique identifier for the company
    public string Name { get; set; }               // Name of the company
    public string Description { get; set; }        // Description of the company
    public string OwnerId { get; set; }            // Reference to the Boss/Owner of the company (ApplicationUser)

    // Navigation properties
    public ApplicationUser Owner { get; set; }     // The owner of the company (Boss)
    public List<ApplicationUser> Employees { get; set; } // Employees working under the company (PMs and regular employees)
}

