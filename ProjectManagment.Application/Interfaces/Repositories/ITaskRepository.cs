using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Repositories;

public interface ITaskRepository
{
    Task<IQueryable<ProjectTask>> GetAll(int pageNumber, int pageSize);
    Task<ProjectTask> GetById(int id);
    Task Add(ProjectTask projectTask);
    Task Update(ProjectTask projectTask);
    Task Delete(ProjectTask projectTask);
}