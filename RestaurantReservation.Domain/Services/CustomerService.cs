using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Customer;

namespace RestaurantReservation.Domain.Services;

public class CustomerService : CRUDService<Customer,CustomerDto>, ICustomerService
{
    public CustomerService(ICRUDRepository<Customer> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}