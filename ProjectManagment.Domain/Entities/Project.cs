namespace ProjectManagment.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Employee Manager { get; set; }
    public int ManagerId { get; set; }
    
    public ClientCompany Client {get; set;}
    public int ClientId { get; set; }
    
    public SupplierCompany Supplier {get; set;}
    public int SupplierId { get; set; }
}