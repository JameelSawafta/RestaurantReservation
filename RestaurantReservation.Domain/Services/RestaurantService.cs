using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Restaurant;

namespace RestaurantReservation.Domain.Services;

public class RestaurantService : IRestaurantService
{
    
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<RestaurantDto>> GetAllRestaurantsAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (restaurants, totalItemCount) = await _restaurantRepository.GetAllAsync(pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);

        var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return new PaginatedList<RestaurantDto>(restaurantDtos.ToList(), pageData);
    }

    public async Task<RestaurantDto> GetRestaurantByIdAsync(Guid id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        return _mapper.Map<RestaurantDto>(restaurant);
    }

    public async Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto restaurantDto)
    {
        var restaurant = _mapper.Map<Restaurant>(restaurantDto);
        var createdRestaurant = await _restaurantRepository.CreateAsync(restaurant);
        return _mapper.Map<RestaurantDto>(createdRestaurant);
    }

    public async Task<RestaurantDto> UpdateRestaurantAsync(Guid id, UpdateRestaurantDto restaurantDto)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null) return null;

        _mapper.Map(restaurantDto, restaurant);
        var updatedRestaurant = await _restaurantRepository.UpdateAsync(restaurant);
        return _mapper.Map<RestaurantDto>(updatedRestaurant);
    }

    public async Task<bool> DeleteRestaurantAsync(Guid id)
    {
        return await _restaurantRepository.DeleteAsync(id);
    }
}