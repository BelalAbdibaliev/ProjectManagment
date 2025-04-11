using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.Enums;

namespace ProjectManagment.Application.Dtos;

public class CreateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public int AuthorId { get; set; }
    public int ManagerId { get; set; }
    public int ProjectId { get; set; }
}