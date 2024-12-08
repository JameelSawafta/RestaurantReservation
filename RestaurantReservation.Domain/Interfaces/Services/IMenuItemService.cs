using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.MenuItem;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IMenuItemService
{
    Task<PaginatedList<MenuItemDto>> GetAllMenuItemsAsync(int pageNumber, int pageSize, string baseUrl);
    Task<MenuItemDto> GetMenuItemByIdAsync(Guid id);
    Task<MenuItemDto> CreateMenuItemAsync(CreateMenuItemDto menuItemDto);
    Task<MenuItemDto> UpdateMenuItemAsync(Guid id, UpdateMenuItemDto menuItemDto);
    Task<bool> DeleteMenuItemAsync(Guid id);
}