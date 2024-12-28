namespace RestaurantReservation.Domain.Models.Table;

public class CreateAndUpdateTableDto
{
    public Guid RestaurantId { get; set; }
    public int Capacity { get; set; }
}