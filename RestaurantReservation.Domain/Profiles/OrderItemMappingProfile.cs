using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Models.OrderItem;

namespace RestaurantReservation.Domain.Profiles;

public class OrderItemMappingProfile : Profile
{
    public OrderItemMappingProfile()
    {
        CreateMap<OrderItem, OrderItemDto>().ReverseMap(); 
        CreateMap<CreateAndUpdateOrderItemDto, OrderItem>();
    }
}