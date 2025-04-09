using ProjectManagment.Application.Interfaces.Repositories;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Persistence.Context;

namespace ProjectManagment.Persistence.Repositories;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IQueryable<Employee>> GetAll(int pageNumber, int pageSize)
    {
        return _context.Employees
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<Employee> GetById(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task Add(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public async Task Update(Employee employee)
    {
        _context.Employees.Update(employee);
    }

    public async Task Delete(int employeeId)
    {
        var employee = await _context.Employees.FindAsync(employeeId);
        _context.Employees.Remove(employee);
    }
}