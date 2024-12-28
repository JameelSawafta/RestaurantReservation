using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.OrderItem;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IOrderItemService : ICRUDService<OrderItemDto,CreateAndUpdateOrderItemDto>
{
}