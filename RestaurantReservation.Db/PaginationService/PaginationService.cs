using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Domain.Services;

public class PaginationService
{
    public async Task<(IEnumerable<T> Items, int TotalCount)> PaginateAsync<T>(
        IQueryable<T> query, int pageNumber, int pageSize) where T : class
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            var totalCount0 = await query.CountAsync();
            var items0 = await query
                .ToListAsync();
            return (items0, totalCount0);
        }
            

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}