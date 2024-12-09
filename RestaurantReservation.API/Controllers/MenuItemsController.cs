using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.MenuItem;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/menuItem")]
[ApiVersion(1.0)]
[Authorize]
public class MenuItemsController : Controller
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemsController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }
    
    [HttpGet]
    public async Task<ActionResult<PaginatedList<MenuItemDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedMenuItems = await _menuItemService.GetAllMenuItemsAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedMenuItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItemDto>> GetById(Guid id)
    {
        var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
        if (menuItem == null) return NotFound();
        return Ok(menuItem);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuItemDto menuItemDto)
    {
        var createdMenuItem = await _menuItemService.CreateMenuItemAsync(menuItemDto);
        return CreatedAtAction(nameof(GetById), new { id = createdMenuItem.ItemId }, createdMenuItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MenuItemDto>> Update(Guid id, UpdateMenuItemDto menuItemDto)
    {
        var updatedMenuItem = await _menuItemService.UpdateMenuItemAsync(id, menuItemDto);
        if (updatedMenuItem == null) return NotFound();
        return Ok(updatedMenuItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _menuItemService.DeleteMenuItemAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}