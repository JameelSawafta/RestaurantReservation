namespace RestaurantReservation.Domain.Models.OrderItem;

public class CreateAndUpdateOrderItemDto
{
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
}