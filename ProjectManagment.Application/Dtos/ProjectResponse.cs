namespace ProjectManagment.Application.Dtos;

public class ProjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Priority { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ProjectLeadId { get; set; }
    public int ClientId { get; set; }
    public int SupplierId { get; set; }
}