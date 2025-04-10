using System.ComponentModel.DataAnnotations;

namespace ProjectManagment.Application.Dtos;

public class CreateProjectDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public int Priority { get; set; }
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int SupplierId { get; set; }
    [Required]
    public int ProjectLeadId { get; set; }
    public int EmployeeId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}