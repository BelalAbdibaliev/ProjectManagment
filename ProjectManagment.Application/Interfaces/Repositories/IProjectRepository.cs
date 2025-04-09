using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<IQueryable<Project>> GetAll(int pageNumber, int pageSize);
    Task<Project> GetById(int id);
    Task Add(Project project);
    Task Update(Project project);
    Task Delete(int projectId);
}