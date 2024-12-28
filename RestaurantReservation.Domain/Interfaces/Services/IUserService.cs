using RestaurantReservation.Domain.Entities;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IUserService
{
    User? ValidateUserCredential(string? userName, string? password);
}