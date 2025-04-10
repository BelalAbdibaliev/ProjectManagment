using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAll(int page, int pageSize);
    Task<Employee> GetById(int id);
    Task Add(Employee employee);
    Task Update(Employee employee);
    Task Delete(Employee employee);
    Task<Employee?> FindByEmail(string email);
}