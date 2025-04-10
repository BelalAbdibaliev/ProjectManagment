namespace ProjectManagment.Application.Dtos;

public class UpdateProjectDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Priority { get; set; } = 0;
    public int ClientId { get; set; } = 0;
    public int SupplierId { get; set; } = 0;
    public int EmployeeId { get; set; } = 0;
}    