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
    public async Task<IActionResult> GetAllProjects(int page = 1, int pageSize = 10)
    {
        var projects = await _projectService.GetAllProjectsAsync(page, pageSize);

        return Ok(projects);
    }

    [HttpGet]
    [Route("getbyid")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        return Ok(project);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateProject(CreateProjectDto projectDto)
    {
        await _projectService.CreateProjectAsync(projectDto);
        return Ok("Project created successfully!");
    }

    [HttpPatch]
    [Route("update")]
    public async Task<IActionResult> UpdateProject(UpdateProjectDto projectDto)
    {
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
}