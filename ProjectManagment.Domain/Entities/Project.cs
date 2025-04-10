namespace ProjectManagment.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Priority { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Employee ProjectLead { get; set; } = null!;
    public int ProjectLeadId { get; set; }
    
    public ClientCompany Client {get; set;}
    public int ClientId { get; set; }
    
    public SupplierCompany Supplier {get; set;}
    public int SupplierId { get; set; }
    
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}