using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/order")]
[ApiVersion("1.0")]
[Authorize]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedOrders = await _orderService.GetAllOrdersAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedOrders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto orderDto)
    {
        var createdOrder = await _orderService.CreateOrderAsync(orderDto);
        return CreatedAtAction(nameof(GetById), new { id = createdOrder.OrderId }, createdOrder);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderDto>> Update(Guid id, UpdateOrderDto orderDto)
    {
        var updatedOrder = await _orderService.UpdateOrderAsync(id, orderDto);
        if (updatedOrder == null) return NotFound();
        return Ok(updatedOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _orderService.DeleteOrderAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}