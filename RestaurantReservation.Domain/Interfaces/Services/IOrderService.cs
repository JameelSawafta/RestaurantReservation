using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IOrderService: ICRUDService<OrderDto,CreateAndUpdateOrderDto>
{
}