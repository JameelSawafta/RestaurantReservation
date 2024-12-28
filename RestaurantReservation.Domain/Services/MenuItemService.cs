using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.MenuItem;

namespace RestaurantReservation.Domain.Services;

public class MenuItemService : CRUDService<MenuItem,MenuItemDto,CreateAndUpdateMenuItemDto>, IMenuItemService
{
    public MenuItemService(ICRUDRepository<MenuItem> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}