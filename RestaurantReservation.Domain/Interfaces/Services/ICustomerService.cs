using RestaurantReservation.Domain.Models.Customer;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto> GetCustomerByIdAsync(Guid id);
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customerDto);
    Task<CustomerDto> UpdateCustomerAsync(Guid id, UpdateCustomerDto customerDto);
    Task<bool> DeleteCustomerAsync(Guid id);
}