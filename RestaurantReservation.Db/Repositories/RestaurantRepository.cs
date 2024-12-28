using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Db.Repositories;

public class RestaurantRepository : CRUDRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(RestaurantReservationDbContext context, PaginationService paginationService) : base(context, paginationService)
    {
    }
}