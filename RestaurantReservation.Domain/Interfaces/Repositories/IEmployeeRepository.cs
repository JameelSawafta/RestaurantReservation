using RestaurantReservation.Domain.Entities;

namespace RestaurantReservation.Domain.Interfaces.Repositories;

public interface IEmployeeRepository : ICRUDRepository<Employee>
{
    Task<(IEnumerable<Employee>, int TotalCount)> GetManagersAsync(int pageNumber, int pageSize);
}