using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Employee;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IEmployeeService : ICRUDService<EmployeeDto,CreateAndUpdateEmployeeDto>
{
    Task<PaginatedList<EmployeeDto>> GetManagersAsync(int pageNumber, int pageSize);
    Task<decimal> GetAverageOrderAmountByEmployeeAsync(Guid employeeId);
}