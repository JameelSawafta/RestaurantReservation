using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository : CRUDRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
    
    public async Task<(IEnumerable<Reservation>, int TotalCount)> GetReservationsByCustomerIdAsync(Guid customerId,int pageNumber, int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
            throw new ArgumentException("PageNumber and PageSize must be greater than 0.");

        var totalCount = await _context.Reservations
            .Where(r => r.CustomerId == customerId).CountAsync();
        var items = await _context.Reservations
            .Where(r => r.CustomerId == customerId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}