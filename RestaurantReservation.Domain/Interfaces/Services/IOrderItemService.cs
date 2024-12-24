using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.OrderItem;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IOrderItemService
{
    Task<PaginatedList<OrderItemDto>> GetAllOrderItemsAsync(int pageNumber, int pageSize, string baseUrl);
    Task<OrderItemDto> GetOrderItemByIdAsync(Guid orderItemId);
    Task<OrderItemDto> CreateOrderItemAsync(OrderItemDto orderItemDto);
    Task<OrderItemDto> UpdateOrderItemAsync(Guid orderItemId, OrderItemDto orderItemDto);
    Task<bool> DeleteOrderItemAsync(Guid orderItemId);
}