using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class TableRepository : CRUDRepository<Table>, ITableRepository
{
    public TableRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
}