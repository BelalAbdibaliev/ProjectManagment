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

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<IActionResult> GetAllTasks([FromQuery] ProjectTaskStatus status, int page = 1, int pageSize = 10)
    {
        var tasks = await _taskService.GetAllTasksAsync(page, pageSize);
        tasks = tasks.Where(s => s.Status == status)
            .OrderBy(s => s.Name);
        
        return Ok(await tasks.ToListAsync());
    }

    [HttpGet]
    [Route("getbyid")]
    public async Task<IActionResult> GetTaskById([FromQuery] int id)
    {
        var task = await _taskService.GetTaskById(id);
        if (task == null)
            return NotFound();
        
        return Ok(task);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto taskDto)
    {
        await _taskService.CreateTaskAsync(taskDto);
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteTask([FromQuery] int id)
    {
        await _taskService.DeleteTaskAsync(id);
        return Ok();
    }

    [HttpPatch]
    [Route("update")]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto taskDto)
    {
        await _taskService.UpdateTaskAsync(taskDto);
        return Ok();
    }
}