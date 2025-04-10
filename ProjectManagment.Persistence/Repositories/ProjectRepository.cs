using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        var existingProject = await _db.Projects.FindAsync(project.Id);
        if (existingProject == null)
            throw new KeyNotFoundException($"Project with {project.Id} not found");
        
        if (!string.IsNullOrEmpty(project.Name))
            existingProject.Name = project.Name;
    
        if (project.Priority != 0)
            existingProject.Priority = project.Priority;
    
        if (project.ClientId != 0)
            existingProject.ClientId = project.ClientId;
    
        if (project.SupplierId != 0)
            existingProject.SupplierId = project.SupplierId;
        
        if(project.ProjectLeadId != 0)
            existingProject.ProjectLeadId = project.ProjectLeadId;
    }

    public async Task Delete(int projectId)
    {
        var project = await _db.Projects.FindAsync(projectId);
        _db.Projects.Remove(project);
    }

    public async Task AddEmployee(Project project, Employee employee)
    {
        project.Employees.Add(employee);
    }
    public async Task RemoveEmployee(Project project, Employee employee)
    {
        if (project != null && employee != null)
        {
            var existingEmployee = project.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEmployee != null)
            {
                project.Employees.Remove(existingEmployee);
            }
        }
    }

    public async Task<Project> GetEmployeesByProject(int projectId)
    {
        return await _db.Projects
            .Include(p => p.Employees)
            .FirstOrDefaultAsync(p => p.Id == projectId);
    }
}