namespace ProjectManagment.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Project> LeadProjects { get; set; } = new List<Project>();
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    public ICollection<ProjectTask> AuthoredTasks { get; set; } = new List<ProjectTask>();
}