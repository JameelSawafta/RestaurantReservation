using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.MenuItem;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IMenuItemService : ICRUDService<MenuItemDto,CreateAndUpdateMenuItemDto>
{
   
}