using ProjectManagment.Application.Dtos;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Interfaces.Services;

public interface ITaskService
{
    Task<IQueryable<ProjectTask>> GetAllTasksAsync(int pageNumber, int pageSize);
    Task<ProjectTask> GetTaskById(int projectTaskId);
    Task CreateTaskAsync(CreateTaskDto task);
    Task UpdateTaskAsync(UpdateTaskDto task);
    Task DeleteTaskAsync(int taskId);
}