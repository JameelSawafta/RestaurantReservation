using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Employee;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/employee")]
[ApiVersion("1.0")]
[Authorize]
public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpGet]
    public async Task<PaginatedList<EmployeeDto>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedEmployees = await _employeeService.GetAllAsync(pageNumber, pageSize);
        return paginatedEmployees;
    }

    [HttpGet("{id}")]
    public async Task<EmployeeDto> GetById(Guid id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        return employee;
    }

    [HttpPost]
    public async Task<EmployeeDto> Create(CreateAndUpdateEmployeeDto employeeDto)
    {
        var createdEmployee = await _employeeService.CreateAsync(employeeDto);
        return createdEmployee;
    }

    [HttpPut("{id}")]
    public async Task<EmployeeDto> Update(Guid id, CreateAndUpdateEmployeeDto employeeDto)
    {
        var updatedEmployee = await _employeeService.UpdateAsync(id, employeeDto);
        return updatedEmployee;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _employeeService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
    
    [HttpGet("managers")]
    public async Task<PaginatedList<EmployeeDto>> GetAllManagers(int pageNumber, int pageSize)
    {
        
        var paginatedManagers = await _employeeService.GetManagersAsync(pageNumber, pageSize);
        return paginatedManagers;
    }
    
    [HttpGet("{employeeId}/average-order-amount")]
    public async Task<IActionResult> GetAverageOrderAmountByEmployee(Guid employeeId)
    {
        var averageOrderAmount = await _employeeService.GetAverageOrderAmountByEmployeeAsync(employeeId);
        return Ok(new { EmployeeId = employeeId, AverageOrderAmount = averageOrderAmount });
    }
}