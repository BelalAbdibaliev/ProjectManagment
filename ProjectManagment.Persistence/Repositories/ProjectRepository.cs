using ProjectManagment.Application.Interfaces.Repositories;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Persistence.Context;

namespace ProjectManagment.Persistence.Repositories;

public class ProjectRepository: IProjectRepository
{
    private readonly ApplicationDbContext _db;
    
    public ProjectRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<IQueryable<Project>> GetAll(int pageNumber, int pageSize)
    {
        return _db.Projects
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<Project> GetById(int id)
    {
        return await _db.Projects.FindAsync(id);
    }

    public async Task Add(Project project)
    {
        await _db.Projects.AddAsync(project);
    }

    public async Task Update(Project project)
    {
        _db.Projects.Update(project);
    }

    public async Task Delete(int projectId)
    {
        var project = await _db.Projects.FindAsync(projectId);
        _db.Projects.Remove(project);
    }
}