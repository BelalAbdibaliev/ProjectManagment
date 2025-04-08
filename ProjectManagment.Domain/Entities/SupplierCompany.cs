namespace ProjectManagment.Domain.Entities;

public class SupplierCompany
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}