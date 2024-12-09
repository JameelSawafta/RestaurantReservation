namespace RestaurantReservation.Domain.Models.OrderItem;

public class DetailedOrderItemDto
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    
    public Entities.MenuItem MenuItem { get; set; }
}