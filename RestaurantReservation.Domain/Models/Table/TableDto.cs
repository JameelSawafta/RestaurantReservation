namespace RestaurantReservation.Domain.Models.Table;

public class TableDto
{
    public Guid TableId { get; set; }
    public Guid RestaurantId { get; set; }
    public int Capacity { get; set; }
}