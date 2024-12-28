namespace RestaurantReservation.Domain.Models.Restaurant;

public class CreateAndUpdateRestaurantDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string OpeningHours { get; set; } 
}