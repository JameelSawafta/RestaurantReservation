using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Customer;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/customer")]
[ApiVersion(1.0)]
public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<CustomerDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
        
        var paginatedCustomers = await _customerService.GetAllCustomersAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedCustomers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetById(Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto customerDto)
    {
        var createdCustomer = await _customerService.CreateCustomerAsync(customerDto);
        return CreatedAtAction(nameof(GetById), new { id = createdCustomer.CustomerId }, createdCustomer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerDto>> Update(Guid id, UpdateCustomerDto customerDto)
    {
        var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customerDto);
        if (updatedCustomer == null) return NotFound();
        return Ok(updatedCustomer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _customerService.DeleteCustomerAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}