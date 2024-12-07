using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class CRUDRepository<T> : ICRUDRepository<T> where T : class
{
    protected readonly RestaurantReservationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public CRUDRepository(RestaurantReservationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public async Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
            throw new ArgumentException("PageNumber and PageSize must be greater than 0.");

        var totalCount = await _dbSet.CountAsync();
        var items = await _dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}