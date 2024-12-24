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
    public async Task<ActionResult<PaginatedList<MenuItemDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        // add the logic to get all menu items with pagination
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedMenuItems = await _menuItemService.GetAllMenuItemsAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedMenuItems);
    }

    [HttpGet("{id}")]
    public async Task<MenuItemDto> GetById(Guid id)
    {
        return await _menuItemService.GetMenuItemByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuItemDto menuItemDto)
    {
        var createdMenuItem = await _menuItemService.CreateMenuItemAsync(menuItemDto);
        // why?
        return CreatedAtAction(nameof(GetById), new { id = createdMenuItem.ItemId }, createdMenuItem);
    }

    [HttpPut("{id}")]
    public async Task<MenuItemDto> Update(Guid id, UpdateMenuItemDto menuItemDto)
    {
        return await _menuItemService.UpdateMenuItemAsync(id, menuItemDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _menuItemService.DeleteMenuItemAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}