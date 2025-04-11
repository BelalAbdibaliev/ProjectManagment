using ProjectManagment.Application.Dtos;
using ProjectManagment.Domain.Entities;
using ProjectManagment.Domain.Enums;

namespace ProjectManagment.Application.Interfaces.Services;

public interface ITaskService
{
    Task<IQueryable<TaskResponseDto>> GetAllTasksAsync(ProjectTaskStatus taskStatus ,int pageNumber, int pageSize);
    Task<TaskResponseDto> GetTaskById(int projectTaskId);
    Task CreateTaskAsync(CreateTaskDto task);
    Task UpdateTaskAsync(UpdateTaskDto task);
    Task DeleteTaskAsync(int taskId);
}