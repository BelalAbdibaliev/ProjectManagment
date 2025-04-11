using AutoMapper;
using ProjectManagment.Application.Dtos;
using ProjectManagment.Application.Interfaces.Repositories;
using ProjectManagment.Application.Interfaces.Services;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Services;

public class ProjectService: IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IQueryable<ProjectResponse>> GetAllProjectsAsync(int pageNumber, int pageSize)
    {
        var projects = await _unitOfWork.Projects.GetAll(pageNumber, pageSize);
        return projects.Select(p => _mapper.Map<ProjectResponse>(p));
    }

    public async Task<ProjectResponse> GetProjectByIdAsync(int id)
    {
        var project = await _unitOfWork.Projects.GetById(id);
        return _mapper.Map<ProjectResponse>(project);
    }

    public async Task CreateProjectAsync(CreateProjectDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        await _unitOfWork.Projects.Add(project);
        await _unitOfWork.SaveAsync();

        if (projectDto.EmployeeId is not 0)
        {
            var employee = await _unitOfWork.Employees.GetById(projectDto.EmployeeId);
            project.Employees.Add(employee);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task UpdateProjectAsync(UpdateProjectDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        await _unitOfWork.Projects.Update(project);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteProjectAsync(int projectId)
    {
        var project = await _unitOfWork.Projects.GetById(projectId);
        if (project == null)
            throw new Exception("Project not found");
        
        await _unitOfWork.Projects.Delete(project);
        await _unitOfWork.SaveAsync();
    }

    public async Task AddEmployee(int projectId, int employeeId)
    {
        var employee = await _unitOfWork.Employees.GetById(employeeId);
        if (employee == null)
            throw new NullReferenceException("Employee not found");
        
        var project = await _unitOfWork.Projects.GetById(projectId);
        if (project == null)
            throw new NullReferenceException("Project not found");
        
        await _unitOfWork.Projects.AddEmployee(project, employee);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoveEmployee(int projectId, int employeeId)
    {
        var employee = await _unitOfWork.Employees.GetById(employeeId);
        if (employee == null)
            throw new NullReferenceException("Employee not found");

        var project = await _unitOfWork.Projects.GetEmployeesByProject(projectId);
        if (project == null)
            throw new NullReferenceException("Project not found");
        
        await _unitOfWork.Projects.RemoveEmployee(project, employee);
        await _unitOfWork.SaveAsync();
    }
}