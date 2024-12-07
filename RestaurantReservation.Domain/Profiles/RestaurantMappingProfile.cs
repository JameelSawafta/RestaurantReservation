using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Models.Restaurant;

namespace RestaurantReservation.Domain.Profiles;

public class RestaurantMappingProfile : Profile
{
    public RestaurantMappingProfile()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<UpdateRestaurantDto, Restaurant>();
    }
}