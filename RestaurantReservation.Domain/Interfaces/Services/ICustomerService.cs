using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Customer;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<PaginatedList<CustomerDto>> GetAllCustomersAsync(int pageNumber, int pageSize, string baseUrl);
    Task<CustomerDto> GetCustomerByIdAsync(Guid id);
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customerDto);
    Task<CustomerDto> UpdateCustomerAsync(Guid id, UpdateCustomerDto customerDto);
    Task<bool> DeleteCustomerAsync(Guid id);
}