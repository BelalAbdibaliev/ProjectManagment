namespace ProjectManagment.Domain.Entities;

public class ClientCompany
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}