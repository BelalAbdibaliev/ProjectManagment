using ProjectManagment.Application.Interfaces.Repositories;
using ProjectManagment.Persistence.Context;

namespace ProjectManagment.Persistence.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IProjectRepository Projects { get; set; }
    public IEmployeeRepository Employees { get; set; }

    public UnitOfWork(
        ApplicationDbContext db,
        IProjectRepository projects,
        IEmployeeRepository employees)
    {
        _db = db;
        Projects = projects;
        Employees = employees;
    }
    
    public async Task<int> SaveAsync()
    {
        return await _db.SaveChangesAsync();
    }
}