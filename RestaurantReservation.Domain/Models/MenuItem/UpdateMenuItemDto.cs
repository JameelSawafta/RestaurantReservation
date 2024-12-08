namespace RestaurantReservation.Domain.Models.MenuItem;

public class UpdateMenuItemDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}