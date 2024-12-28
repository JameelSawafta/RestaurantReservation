using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Restaurant;

namespace RestaurantReservation.Domain.Services;

public class RestaurantService : CRUDService<Restaurant,RestaurantDto,CreateAndUpdateRestaurantDto>,  IRestaurantService
{
    public RestaurantService(ICRUDRepository<Restaurant> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}