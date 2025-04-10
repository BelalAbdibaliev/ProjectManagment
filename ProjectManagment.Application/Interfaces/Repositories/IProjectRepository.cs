using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<IQueryable<Project>> GetAll(int pageNumber, int pageSize);
    Task<Project> GetById(int id);
    Task Add(Project project);
    Task Update(Project project);
    Task Delete(int projectId);
    Task AddEmployee(Project project, Employee employee);
    Task RemoveEmployee(Project project, Employee employee);
    Task<Project> GetEmployeesByProject(int projectId);
}