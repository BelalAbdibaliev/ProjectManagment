using System.ComponentModel.DataAnnotations;

namespace ProjectManagment.Application.Dtos;

public class CreateEmployeeDto
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string MiddleName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}