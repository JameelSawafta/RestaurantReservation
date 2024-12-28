using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository : CRUDRepository<Reservation>, IReservationRepository
{
    private readonly PaginationService _paginationService;
    public ReservationRepository(RestaurantReservationDbContext context, PaginationService paginationService) : base(context, paginationService)
    {
        _paginationService = paginationService;
    }

    public async Task<(IEnumerable<Reservation>, int TotalCount)> GetReservationsByCustomerIdAsync(Guid customerId,int pageNumber, int pageSize)
    {
        var query = _dbSet.Where(r => r.CustomerId == customerId).AsQueryable();
        var (items, totalItemCount) = await _paginationService.PaginateAsync(query, pageNumber, pageSize);
        return (items, totalItemCount);
    }
    
    public async Task<(IEnumerable<Order>, int TotalCount)> GetOrdersByReservationIdAsync(Guid reservationId, int pageNumber, int pageSize)
    {
        var query = _context.Orders
            .Where(o => o.ReservationId == reservationId)
            .Include(o => o.OrderItems) 
            .ThenInclude(oi => oi.MenuItem)
            .AsQueryable();
        var (items, totalItemCount) = await _paginationService.PaginateAsync(query, pageNumber, pageSize);
        return (items, totalItemCount);
    }
}