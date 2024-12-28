using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Models.Customer;

namespace RestaurantReservation.Domain.Profiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CreateAndUpdateCustomerDto, Customer>();
    }
}