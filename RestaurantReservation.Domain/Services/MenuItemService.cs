using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.MenuItem;

namespace RestaurantReservation.Domain.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMapper _mapper;

    public MenuItemService(IMenuItemRepository menuItemRepository, IMapper mapper)
    {
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<MenuItemDto>> GetAllMenuItemsAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (menuItems, totalItemCount) = await _menuItemRepository.GetAllAsync(pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var menuItemDtos = _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);

        return new PaginatedList<MenuItemDto>(menuItemDtos.ToList(), pageData);
    }

    public async Task<MenuItemDto> GetMenuItemByIdAsync(Guid itemId)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(itemId);
        return menuItem == null ? null : _mapper.Map<MenuItemDto>(menuItem);
    }

    public async Task<MenuItemDto> CreateMenuItemAsync(CreateMenuItemDto menuItemDto)
    {
        var menuItem = _mapper.Map<MenuItem>(menuItemDto);
        var createdMenuItem = await _menuItemRepository.CreateAsync(menuItem);
        return _mapper.Map<MenuItemDto>(createdMenuItem);
    }

    public async Task<MenuItemDto> UpdateMenuItemAsync(Guid itemId, UpdateMenuItemDto menuItemDto)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(itemId);
        if (menuItem == null) return null;

        _mapper.Map(menuItemDto, menuItem);
        var updatedMenuItem = await _menuItemRepository.UpdateAsync(menuItem);
        return _mapper.Map<MenuItemDto>(updatedMenuItem);
    }

    public async Task<bool> DeleteMenuItemAsync(Guid itemId)
    {
        return await _menuItemRepository.DeleteAsync(itemId);
    }
}