using Microsoft.AspNetCore.Mvc;
using ProjectManagment.Application.Dtos;
using ProjectManagment.Application.Interfaces.Services;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Presentation.Controllers;

[ApiController]
[Route("employees/")]
public class EmployeeController: Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<IActionResult> GetAll([FromQuery]int page = 1, [FromQuery]int pageSize = 10)
    {
        var employees = await _employeeService.GetAllEmployeesAsync(page, pageSize);
        _logger.LogInformation($"Got all employees");
        return Ok(employees);
    }

    [HttpGet]
    [Route("getbyid")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        _logger.LogInformation($"Got employee with id: {id}");
        return Ok(employee);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] CreateEmployeeDto employeeDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _employeeService.CreateEmployeeAsync(employeeDto);
        _logger.LogInformation("Added employee");
        return Ok();
    }

    [HttpPatch]
    [Route("update")]
    public async Task<IActionResult> Update(UpdateEmployeeDto employeeDto)
    {
        await _employeeService.UpdateEmployeeAsync(employeeDto);
        _logger.LogInformation("Updated employee");
        return Ok("Updated successfully");
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        _logger.LogInformation("Deleted employee");
        return Ok();
    }
}