using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Db.Repositories;

public class CRUDRepository<T> : ICRUDRepository<T> where T : class
{
    protected readonly RestaurantReservationDbContext _context;
    protected readonly DbSet<T> _dbSet;
    private readonly PaginationService _paginationService;

    public CRUDRepository(RestaurantReservationDbContext context, PaginationService paginationService)
    {
        _context = context;
        _paginationService = paginationService;
        _dbSet = _context.Set<T>();
    }


    public async Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _dbSet.AsQueryable();
        var (items, totalCount) = await _paginationService.PaginateAsync(query, pageNumber, pageSize);
        if (totalCount == 0)
        {
            throw new KeyNotFoundException($"{nameof(T)} with not data found.");
        }
        return (items, totalCount);
    }

    public async Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync()
    {
        var data = await _dbSet.ToListAsync();
        if (data.Count == 0)
        {
            throw new KeyNotFoundException($"{nameof(T)} with not data found.");
        }
        return (data, data.Count);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var data = await _dbSet.FindAsync(id);
        if (data == null)
        {
            throw new KeyNotFoundException($"{typeof(T).Name} with ID {id} not found.");
        }
        return data;
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