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
    public async Task<PaginatedList<OrderDto>> GetAll(int pageNumber, int pageSize)
    {
        var paginatedOrders = await _orderService.GetAllAsync(pageNumber, pageSize);
        return paginatedOrders;
    }

    [HttpGet("{id}")]
    public async Task<OrderDto> GetById(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);
        return order;
    }

    [HttpPost]
    public async Task<OrderDto> Create(CreateAndUpdateOrderDto orderDto)
    {
        var createdOrder = await _orderService.CreateAsync(orderDto);
        return createdOrder;
    }

    [HttpPut("{id}")]
    public async Task<OrderDto> Update(Guid id, CreateAndUpdateOrderDto orderDto)
    {
        var updatedOrder = await _orderService.UpdateAsync(id, orderDto);
        return updatedOrder;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _orderService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}