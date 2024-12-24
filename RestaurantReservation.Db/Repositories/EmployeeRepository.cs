using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Enums;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository : CRUDRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
    
    public async Task<(IEnumerable<Employee>, int TotalCount)> GetManagersAsync(int pageNumber, int pageSize)
    {
        // add a special service to validate the pagination parameters, since you used it for multiple services
        // separation of concerns + code reusability
        if (pageNumber < 1 || pageSize < 1)
            throw new ArgumentException("PageNumber and PageSize must be greater than 0.");

        var totalCount = await _context.Employees.Where(m => m.Position == EmployeePosition.Manager).CountAsync();
        var items = await _context.Employees
            .Where(e => e.Position == EmployeePosition.Manager)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
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