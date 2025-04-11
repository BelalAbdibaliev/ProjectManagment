using ProjectManagment.Domain.Enums;

namespace ProjectManagment.Application.Dtos;

public class TaskResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public int AuthorId { get; set; }
    public int ManagerId { get; set; }
    public int ProjectId { get; set; }
    public ProjectTaskStatus Status { get; set; }
}