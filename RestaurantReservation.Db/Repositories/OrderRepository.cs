using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Db.Repositories;

public class OrderRepository : CRUDRepository<Order>, IOrderRepository
{
    public OrderRepository(RestaurantReservationDbContext context, PaginationService paginationService) : base(context, paginationService)
    {
    }
}