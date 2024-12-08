using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class MenuItemRepository : CRUDRepository<MenuItem>, IMenuItemRepository
{
    public MenuItemRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
}