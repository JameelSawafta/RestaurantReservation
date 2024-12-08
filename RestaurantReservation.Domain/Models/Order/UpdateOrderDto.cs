namespace RestaurantReservation.Domain.Models.Order;

public class UpdateOrderDto
{
    public Guid ReservationId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}