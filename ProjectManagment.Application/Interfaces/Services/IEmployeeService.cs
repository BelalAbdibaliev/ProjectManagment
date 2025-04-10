using ProjectManagment.Application.Dtos;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployeesAsync(int page, int pageSize);
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task CreateEmployeeAsync(CreateEmployeeDto employee);
    Task UpdateEmployeeAsync(UpdateEmployeeDto employee);
    Task DeleteEmployeeAsync(int employeeId);
}