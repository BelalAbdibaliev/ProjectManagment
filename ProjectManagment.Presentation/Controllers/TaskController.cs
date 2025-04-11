using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.Application.Dtos;
using ProjectManagment.Application.Interfaces.Services;
using ProjectManagment.Domain.Enums;

namespace ProjectManagment.Presentation.Controllers;

[ApiController]
[Route("tasks/")]
public class TaskController: Controller
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskController> _logger;

    public TaskController(ITaskService taskService, ILogger<TaskController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<IActionResult> GetAllTasks([FromQuery] ProjectTaskStatus status, int page = 1, int pageSize = 10)
    {
        var tasks = await _taskService.GetAllTasksAsync(status ,page, pageSize);
        _logger.LogInformation("Get all tasks");
        return Ok(await tasks.ToListAsync());
    }

    [HttpGet]
    [Route("getbyid")]
    public async Task<IActionResult> GetTaskById([FromQuery] int id)
    {
        var task = await _taskService.GetTaskById(id);
        if (task == null)
            return NotFound();
        
        _logger.LogInformation($"Get task by id: {id}");
        return Ok(task);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto taskDto)
    {
        await _taskService.CreateTaskAsync(taskDto);
        _logger.LogInformation("Created task");
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteTask([FromQuery] int id)
    {
        await _taskService.DeleteTaskAsync(id);
        _logger.LogInformation("Deleted task");
        return Ok();
    }

    [HttpPatch]
    [Route("update")]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto taskDto)
    {
        await _taskService.UpdateTaskAsync(taskDto);
        _logger.LogInformation("Updated task");
        return Ok();
    }
}