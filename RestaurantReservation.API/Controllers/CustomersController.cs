using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Customer;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/customer")]
[ApiVersion("1.0")]
[Authorize]
public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<CustomerDto>>> GetAll(int pageNumber, int pageSize)
    {
        var paginatedCustomers = await _customerService.GetAllAsync(pageNumber, pageSize);
        return Ok(paginatedCustomers);
    }

    [HttpGet("{id}")]
    public async Task<CustomerDto> GetById(Guid id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        return customer;
    }

    [HttpPost]
    public async Task<CustomerDto> Create(CreateAndUpdateCustomerDto customerDto)
    {
        var createdCustomer = await _customerService.CreateAsync(customerDto);
        return createdCustomer;
    }

    [HttpPut("{id}")]
    public async Task<CustomerDto> Update(Guid id, CreateAndUpdateCustomerDto customerDto)
    {
        var updatedCustomer = await _customerService.UpdateAsync(id, customerDto);
        return updatedCustomer;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _customerService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}