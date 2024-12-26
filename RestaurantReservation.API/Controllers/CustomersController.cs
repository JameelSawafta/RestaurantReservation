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
    public async Task<ActionResult<PaginatedList<CustomerDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        // create customer service
        // move this logic to service level
        // add the logic to get all customers with pagination
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        // you should have another way to get the customer name
        // try to search for it
        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
        
        var paginatedCustomers = await _customerService.GetAllCustomersAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedCustomers);
    }

    [HttpGet("{id}")]
    public async Task<CustomerDto> GetById(Guid id)
    {
        // updated to return customer dto
        return await _customerService.GetCustomerByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerDto customerDto)
    {
        var createdCustomer = await _customerService.CreateCustomerAsync(customerDto);
        // why?
        return CreatedAtAction(nameof(GetById), new { id = createdCustomer.CustomerId }, createdCustomer);
    }

    [HttpPut("{id}")]
    public async Task<CustomerDto> Update(Guid id, UpdateCustomerDto customerDto)
    {
        return await _customerService.UpdateCustomerAsync(id, customerDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _customerService.DeleteCustomerAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}