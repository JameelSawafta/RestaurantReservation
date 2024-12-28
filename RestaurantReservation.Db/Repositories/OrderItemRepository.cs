using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Db.Repositories;

public class OrderItemRepository : CRUDRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(RestaurantReservationDbContext context, PaginationService paginationService) : base(context, paginationService)
    {
    }
}