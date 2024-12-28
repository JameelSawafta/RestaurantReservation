using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Db.Repositories;

public class TableRepository : CRUDRepository<Table>, ITableRepository
{
    public TableRepository(RestaurantReservationDbContext context, PaginationService paginationService) : base(context, paginationService)
    {
    }
}