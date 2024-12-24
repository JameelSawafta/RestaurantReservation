using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Models.Order;

namespace RestaurantReservation.Domain.Profiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<Order, DetailedOrderDto>();
    }
}