namespace RestaurantReservation.Domain.Models.OrderItem;

public class CreateOrderItemDto
{
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
}