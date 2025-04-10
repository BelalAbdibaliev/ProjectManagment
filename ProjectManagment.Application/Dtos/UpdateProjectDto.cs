namespace ProjectManagment.Application.Dtos;

public class UpdateProjectDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Priority { get; set; } = 0;
    public int ClientId { get; set; } = 0;
    public int SupplierId { get; set; } = 0;
    public int ProjectLeadId { get; set; } = 0;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}    