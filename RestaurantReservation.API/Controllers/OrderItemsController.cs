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
    public async Task<PaginatedList<OrderItemDto>> GetAll(int pageNumber, int pageSize)
    {
        var paginatedOrderItems = await _orderItemService.GetAllAsync(pageNumber, pageSize);
        return paginatedOrderItems;
    }

    [HttpGet("{id}")]
    public async Task<OrderItemDto> GetById(Guid id)
    {
        var orderItem = await _orderItemService.GetByIdAsync(id);
        return orderItem;
    }

    [HttpPost]
    public async Task<OrderItemDto> Create(CreateAndUpdateOrderItemDto orderItemDto)
    {
        var createdOrderItem = await _orderItemService.CreateAsync(orderItemDto);
        return createdOrderItem;
    }

    [HttpPut("{id}")]
    public async Task<OrderItemDto> Update(Guid id, CreateAndUpdateOrderItemDto orderItemDto)
    {
        var updatedOrderItem = await _orderItemService.UpdateAsync(id, orderItemDto);
        return updatedOrderItem;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _orderItemService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}