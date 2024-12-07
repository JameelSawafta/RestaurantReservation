namespace RestaurantReservation.Domain.Interfaces.Repositories;

public interface ICRUDRepository<T> where T : class
{
    Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
    Task<T> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}