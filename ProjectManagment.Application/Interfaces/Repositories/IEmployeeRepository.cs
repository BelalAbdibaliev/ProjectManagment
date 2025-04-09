using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IQueryable<Employee>> GetAll(int pageNumber, int pageSize);
    Task<Employee> GetById(int id);
    Task Add(Employee employee);
    Task Update(Employee employee);
    Task Delete(int employeeId);
}