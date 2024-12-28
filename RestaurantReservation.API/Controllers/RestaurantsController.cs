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
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<PaginatedList<RestaurantDto>> GetAll(int pageNumber, int pageSize)
    {
        var paginatedRestaurants = await _restaurantService.GetAllAsync(pageNumber, pageSize);
        return paginatedRestaurants;
    }

    [HttpGet("{id}")]
    public async Task<RestaurantDto> GetById(Guid id)
    {
        var restaurant = await _restaurantService.GetByIdAsync(id);
        return restaurant;
    }

    [HttpPost]
    public async Task<RestaurantDto> Create(CreateAndUpdateRestaurantDto restaurantDto)
    {
        var createdRestaurant = await _restaurantService.CreateAsync(restaurantDto);
        return createdRestaurant;
    }

    [HttpPut("{id}")]
    public async Task<RestaurantDto> Update(Guid id, CreateAndUpdateRestaurantDto restaurantDto)
    {
        var updatedRestaurant = await _restaurantService.UpdateAsync(id, restaurantDto);
        return updatedRestaurant;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _restaurantService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}