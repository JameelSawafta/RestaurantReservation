using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.MenuItem;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/menuItem")]
[ApiVersion("1.0")]
[Authorize]
public class MenuItemsController : Controller
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemsController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }
    
    [HttpGet]
    public async Task<PaginatedList<MenuItemDto>> GetAll(int pageNumber, int pageSize)
    {
        var paginatedMenuItem = await _menuItemService.GetAllAsync(pageNumber, pageSize);
        return paginatedMenuItem;
    }

    [HttpGet("{id}")]
    public async Task<MenuItemDto> GetById(Guid id)
    {
        var menuItem = await _menuItemService.GetByIdAsync(id);
        return menuItem;
    }

    [HttpPost]
    public async Task<MenuItemDto> Create(CreateAndUpdateMenuItemDto menuItemDto)
    {
        var createdMenuItem = await _menuItemService.CreateAsync(menuItemDto);
        return createdMenuItem;
    }

    [HttpPut("{id}")]
    public async Task<MenuItemDto> Update(Guid id, CreateAndUpdateMenuItemDto menuItemDto)
    {
        var updatedMenuItem = await _menuItemService.UpdateAsync(id, menuItemDto);
        return updatedMenuItem;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _menuItemService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}