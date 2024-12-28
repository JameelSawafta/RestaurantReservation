using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Services;

namespace RestaurantReservation.Domain.Services;

public class UserService : IUserService
{
    public User? ValidateUserCredential(string? userName, string? password)
    {
        if (userName == "jameel" && password == "123456")
        {
            return new User(1, userName ?? "", "Jameel", "Sawafta");
        }
        return null;
    }
}