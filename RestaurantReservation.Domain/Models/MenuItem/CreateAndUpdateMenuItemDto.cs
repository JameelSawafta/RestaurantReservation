namespace RestaurantReservation.Domain.Models.MenuItem;

public class CreateAndUpdateMenuItemDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid RestaurantId { get; set; }
}