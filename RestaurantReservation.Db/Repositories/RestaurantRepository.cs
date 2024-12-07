using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class RestaurantRepository : CRUDRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
}