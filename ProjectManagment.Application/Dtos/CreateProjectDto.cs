namespace ProjectManagment.Application.Dtos;

public class CreateProjectDto
{
    public string Name { get; set; } = null!;
    public int Priority { get; set; }
    public int ClientId { get; set; }
    public int SupplierId { get; set; }
    public int ProjectLeadId { get; set; }
    public int EmployeeId { get; set; }
}