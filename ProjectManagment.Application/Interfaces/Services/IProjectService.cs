using ProjectManagment.Application.Dtos;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Services;

public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync(int pageNumber, int pageSize);
    Task<Project> GetProjectByIdAsync(int id);
    Task CreateProjectAsync(CreateProjectDto project);
    Task UpdateProjectAsync(UpdateProjectDto project);
    Task DeleteProjectAsync(int projectId);
}