using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Customer;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface ICustomerService : ICRUDService<CustomerDto,CreateAndUpdateCustomerDto>
{
    
}