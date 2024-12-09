using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Employee;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/employee")]
[ApiVersion(1.0)]
[Authorize]
public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpGet]
    public async Task<ActionResult<PaginatedList<EmployeeDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedEmployees = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedEmployees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDto employeeDto)
    {
        var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeDto);
        return CreatedAtAction(nameof(GetById), new { id = createdEmployee.EmployeeId }, createdEmployee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDto>> Update(Guid id, UpdateEmployeeDto employeeDto)
    {
        var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
        if (updatedEmployee == null) return NotFound();
        return Ok(updatedEmployee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _employeeService.DeleteEmployeeAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
    
    [HttpGet("managers")]
    public async Task<ActionResult<PaginatedList<EmployeeDto>>> GetAllManagers(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedManagers = await _employeeService.GetManagersAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedManagers);
    }
    
    [HttpGet("{employeeId}/average-order-amount")]
    public async Task<IActionResult> GetAverageOrderAmountByEmployee(Guid employeeId)
    {
        var averageOrderAmount = await _employeeService.GetAverageOrderAmountByEmployeeAsync(employeeId);
            
        if (averageOrderAmount == 0)
            return NotFound($"No orders found for employee with ID {employeeId}.");

        return Ok(new { EmployeeId = employeeId, AverageOrderAmount = averageOrderAmount });
    }
}