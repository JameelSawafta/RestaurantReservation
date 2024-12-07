using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Restaurant;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IRestaurantService
{
    Task<PaginatedList<RestaurantDto>> GetAllRestaurantsAsync(int pageNumber, int pageSize, string baseUrl);
    Task<RestaurantDto> GetRestaurantByIdAsync(Guid id);
    Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto restaurantDto);
    Task<RestaurantDto> UpdateRestaurantAsync(Guid id, UpdateRestaurantDto restaurantDto);
    Task<bool> DeleteRestaurantAsync(Guid id);
}