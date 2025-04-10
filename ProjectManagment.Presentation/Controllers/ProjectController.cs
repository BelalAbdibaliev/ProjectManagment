using Microsoft.AspNetCore.Mvc;
using ProjectManagment.Application.Dtos;
using ProjectManagment.Application.Interfaces.Services;

namespace ProjectManagment.Presentation.Controllers;

[ApiController]
[Route("projects/")]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<IActionResult> GetAllProjects(
        [FromQuery] DateTime? startDateFrom,
        [FromQuery] DateTime? startDateTo,
        [FromQuery] int? priority,
        int page = 1, int pageSize = 10)
    {
        var projects = await _projectService.GetAllProjectsAsync(page, pageSize);
        
        if (startDateFrom.HasValue)
            projects = projects.Where(p => p.StartDate >= startDateFrom.Value);
        
        if (startDateTo.HasValue)
            projects = projects.Where(p => p.StartDate <= startDateTo.Value);
        
        if (priority.HasValue)
            projects = projects.Where(p => p.Priority == priority.Value || p.Priority > priority.Value);
        
        if(projects is null)
            return NotFound("No projects found");

        return Ok(projects.ToList());
    }

    [HttpGet]
    [Route("getbyid")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if(project is null)
            return NotFound();
        
        return Ok(project);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateProject([FromBody]CreateProjectDto projectDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _projectService.CreateProjectAsync(projectDto);
        return Ok("Project created successfully!");
    }

    [HttpPatch]
    [Route("update")]
    public async Task<IActionResult> UpdateProject([FromBody]UpdateProjectDto projectDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _projectService.UpdateProjectAsync(projectDto);
        return Ok("Project updated successfully!");
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await _projectService.DeleteProjectAsync(id);
        return Ok("Project deleted successfully!");
    }

    [HttpPost]
    [Route("add-employee")]
    public async Task<IActionResult> AddEmployee(int projectId, int employeeId)
    {
        await _projectService.AddEmployee(projectId, employeeId);
        return Ok("Employee added to project successfully!");
    }

    [HttpDelete]
    [Route("remove-employee")]
    public async Task<IActionResult> RemoveEmployee(int projectId, int employeeId)
    {
        await _projectService.RemoveEmployee(projectId, employeeId);
        return Ok("Employee removed from project successfully!");
    }
}