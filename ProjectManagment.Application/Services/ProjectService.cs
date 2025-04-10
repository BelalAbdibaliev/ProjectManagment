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


    public async Task<List<Project>> GetAllProjectsAsync(int pageNumber, int pageSize)
    {
        var projects = await _unitOfWork.Projects.GetAll(pageNumber, pageSize);
        return projects.ToList();
    }

    public async Task<Project> GetProjectByIdAsync(int id)
    {
        var project = await _unitOfWork.Projects.GetById(id);
        return project;
    }

    public async Task CreateProjectAsync(CreateProjectDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        await _unitOfWork.Projects.Add(project);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateProjectAsync(UpdateProjectDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        await _unitOfWork.Projects.Update(project);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteProjectAsync(int projectId)
    {
        await _unitOfWork.Projects.Delete(projectId);
        await _unitOfWork.SaveAsync();
    }
}