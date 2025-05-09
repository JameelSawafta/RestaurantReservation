namespace RestaurantReservation.Domain.Models.MenuItem;

public class MenuItemDto
{
    public Guid ItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid RestaurantId { get; set; }
}