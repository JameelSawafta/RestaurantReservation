using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Employee;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IEmployeeService
{
    Task<PaginatedList<EmployeeDto>> GetAllEmployeesAsync(int pageNumber, int pageSize, string baseUrl);
    Task<EmployeeDto> GetEmployeeByIdAsync(Guid id);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto employeeDto);
    Task<EmployeeDto> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto employeeDto);
    Task<bool> DeleteEmployeeAsync(Guid id);
    
    Task<PaginatedList<EmployeeDto>> GetManagersAsync(int pageNumber, int pageSize, string baseUrl);
}