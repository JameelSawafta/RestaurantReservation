using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Enums;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository : CRUDRepository<Employee>, IEmployeeRepository
{
    private readonly PaginationService _paginationService;

    public EmployeeRepository(RestaurantReservationDbContext context, PaginationService paginationService) : base(context, paginationService)
    {
        _paginationService = paginationService;
    }

    public async Task<(IEnumerable<Employee>, int TotalCount)> GetManagersAsync(int pageNumber, int pageSize)
    {
        var query = _dbSet.Where(m => m.Position == EmployeePosition.Manager).AsQueryable();
        var (items, totalItemCount) = await _paginationService.PaginateAsync(query, pageNumber, pageSize);
        return (items, totalItemCount);
    }
    
    public async Task<decimal> GetAverageOrderAmountByEmployeeAsync(Guid employeeId)
    {
        var orders = await _context.Orders
            .Where(o => o.EmployeeId == employeeId)
            .ToListAsync();

        if (!orders.Any())
            return 0;

        return orders.Average(o => o.TotalAmount);
    }
}