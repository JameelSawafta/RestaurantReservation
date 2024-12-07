namespace RestaurantReservation.Domain.Models.Table;

public class CreateTableDto
{
    public Guid RestaurantId { get; set; }
    public int Capacity { get; set; }
}