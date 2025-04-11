using AutoMapper;
using ProjectManagment.Application.Dtos;
using ProjectManagment.Application.Interfaces.Repositories;
using ProjectManagment.Application.Interfaces.Services;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Services;

public class TaskService: ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IQueryable<ProjectTask>> GetAllTasksAsync(int pageNumber, int pageSize)
    {
        var projects = await _unitOfWork.Tasks.GetAll(pageNumber, pageSize);
        return projects;
    }

    public async Task<ProjectTask> GetTaskById(int projectTaskId)
    {
        return await _unitOfWork.Tasks.GetById(projectTaskId);
    }

    public async Task CreateTaskAsync(CreateTaskDto task)
    {
        var taskEntity = _mapper.Map<ProjectTask>(task);
        
        await _unitOfWork.Tasks.Add(taskEntity);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateTaskAsync(UpdateTaskDto task)
    {
        var taskEntity = _mapper.Map<ProjectTask>(task);
        
        await _unitOfWork.Tasks.Update(taskEntity);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteTaskAsync(int taskId)
    {
        var taskEntity = await _unitOfWork.Tasks.GetById(taskId);
        if(taskEntity is null)
            throw new NullReferenceException("Task does not exist");
        
        await _unitOfWork.Tasks.Delete(taskEntity);
        await _unitOfWork.SaveAsync();
    }
}