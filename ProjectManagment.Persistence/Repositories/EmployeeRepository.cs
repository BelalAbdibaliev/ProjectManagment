using Microsoft.EntityFrameworkCore;
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
    
    public async Task<List<Employee>> GetAll(int page, int pageSize)
    {
        return await _context.Employees
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
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
       var existingEmployee = await _context.Employees.FindAsync(employee.Id);
       if (existingEmployee == null)
           throw new NullReferenceException("Employee not found");
       
       if(!string.IsNullOrWhiteSpace(employee.FirstName))
           existingEmployee.FirstName = employee.FirstName;

       if(!string.IsNullOrWhiteSpace(employee.LastName))
           existingEmployee.LastName = employee.LastName;

       if(!string.IsNullOrWhiteSpace(employee.Email))
           existingEmployee.Email = employee.Email;

       if(!string.IsNullOrWhiteSpace(employee.MiddleName))
           existingEmployee.MiddleName = employee.MiddleName;
    }

    public async Task Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
    }

    public async Task<Employee?> FindByEmail(string email)
    {
        return await _context.Employees.FirstOrDefaultAsync(x => x.Email == email); 
    }
}