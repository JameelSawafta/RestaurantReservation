using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class OrderItemRepository : CRUDRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
}