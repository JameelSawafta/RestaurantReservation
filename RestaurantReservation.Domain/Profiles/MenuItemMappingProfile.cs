using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Models.MenuItem;

namespace RestaurantReservation.Domain.Profiles;

public class MenuItemMappingProfile : Profile
{
    public MenuItemMappingProfile()
    {
        CreateMap<MenuItem, MenuItemDto>();
        CreateMap<CreateMenuItemDto, MenuItem>();
        CreateMap<UpdateMenuItemDto, MenuItem>();
    }
}