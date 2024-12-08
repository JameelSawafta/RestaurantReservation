using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository : CRUDRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
}