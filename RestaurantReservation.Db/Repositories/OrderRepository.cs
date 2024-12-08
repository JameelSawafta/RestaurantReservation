using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class OrderRepository : CRUDRepository<Order>, IOrderRepository
{
    public OrderRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
}