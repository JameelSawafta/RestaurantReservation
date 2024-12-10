using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.OrderItem;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/OrderItem")]
[ApiVersion("1.0")]
[Authorize]
public class OrderItemsController : Controller
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemsController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderItemDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedOrderItems = await _orderItemService.GetAllOrderItemsAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedOrderItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemDto>> GetById(Guid id)
    {
        var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
        if (orderItem == null) return NotFound();
        return Ok(orderItem);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderItemDto orderItemDto)
    {
        var createdOrderItem = await _orderItemService.CreateOrderItemAsync(orderItemDto);
        return CreatedAtAction(nameof(GetById), new { id = createdOrderItem.OrderItemId }, createdOrderItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderItemDto>> Update(Guid id, UpdateOrderItemDto orderItemDto)
    {
        var updatedOrderItem = await _orderItemService.UpdateOrderItemAsync(id, orderItemDto);
        if (updatedOrderItem == null) return NotFound();
        return Ok(updatedOrderItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _orderItemService.DeleteOrderItemAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}