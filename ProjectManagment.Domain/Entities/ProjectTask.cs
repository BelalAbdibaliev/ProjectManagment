namespace ProjectManagment.Domain.Entities;

public class ProjectTask
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
    public Employee Author { get; set; }
    public int AuthorId { get; set; }
    public Employee Manager { get; set; }
    public int ManagerId { get; set; }
    public Project Project { get; set; }
    public int ProjectId { get; set; }
}