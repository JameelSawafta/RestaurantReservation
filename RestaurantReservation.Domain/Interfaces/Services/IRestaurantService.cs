using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Restaurant;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IRestaurantService: ICRUDService<RestaurantDto,CreateAndUpdateRestaurantDto>
{
}