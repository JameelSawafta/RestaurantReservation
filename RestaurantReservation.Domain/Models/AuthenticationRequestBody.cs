namespace RestaurantReservation.Domain.Models;

public class AuthenticationRequestBody
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}