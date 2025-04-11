using ProjectManagment.Application.Dtos;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Services;

public interface IProjectService
{
    Task<IQueryable<ProjectResponse>> GetAllProjectsAsync(int pageNumber, int pageSize);
    Task<ProjectResponse> GetProjectByIdAsync(int id);
    Task CreateProjectAsync(CreateProjectDto project);
    Task UpdateProjectAsync(UpdateProjectDto project);
    Task DeleteProjectAsync(int projectId);
    Task AddEmployee(int projectId, int employeeId);
    Task RemoveEmployee(int projectId, int employeeId);
}