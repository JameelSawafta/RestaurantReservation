using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<PaginatedList<OrderDto>> GetAllOrdersAsync(int pageNumber, int pageSize, string baseUrl);
    Task<OrderDto> GetOrderByIdAsync(Guid orderId);
    Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
    Task<OrderDto> UpdateOrderAsync(Guid orderId, UpdateOrderDto orderDto);
    Task<bool> DeleteOrderAsync(Guid orderId);
}