using ProjectManagment.Application.Interfaces.Repositories;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Persistence.Context;

namespace ProjectManagment.Persistence.Repositories;

public class TaskRepository: ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IQueryable<ProjectTask>> GetAll(int pageNumber, int pageSize)
    {
        return _context.ProjectTasks
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<ProjectTask> GetById(int id)
    {
        return await _context.ProjectTasks.FindAsync(id);
    }

    public async Task Add(ProjectTask projectTask)
    {
        await _context.ProjectTasks.AddAsync(projectTask);
    }

    public async Task Update(ProjectTask projectTask)
    {
        _context.ProjectTasks.Update(projectTask);
    }

    public async Task Delete(ProjectTask projectTask)
    {
        _context.ProjectTasks.Remove(projectTask);
    }
}