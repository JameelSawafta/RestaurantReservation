using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Restaurant;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/restaurant")]
[ApiVersion("1.0")]
[Authorize]
public class RestaurantsController : Controller
{
    // same as other controller

    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<RestaurantDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedRestaurants = await _restaurantService.GetAllRestaurantsAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedRestaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetById(Guid id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null) return NotFound();
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRestaurantDto restaurantDto)
    {
        var createdRestaurant = await _restaurantService.CreateRestaurantAsync(restaurantDto);
        return CreatedAtAction(nameof(GetById), new { id = createdRestaurant.RestaurantId }, createdRestaurant);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RestaurantDto>> Update(Guid id, UpdateRestaurantDto restaurantDto)
    {
        var updatedRestaurant = await _restaurantService.UpdateRestaurantAsync(id, restaurantDto);
        if (updatedRestaurant == null) return NotFound();
        return Ok(updatedRestaurant);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _restaurantService.DeleteRestaurantAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}