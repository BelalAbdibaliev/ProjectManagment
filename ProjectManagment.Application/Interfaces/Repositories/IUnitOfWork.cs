namespace ProjectManagment.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IProjectRepository Projects { get; set; }
    IEmployeeRepository Employees { get; set; }

    Task<int> SaveAsync();
}