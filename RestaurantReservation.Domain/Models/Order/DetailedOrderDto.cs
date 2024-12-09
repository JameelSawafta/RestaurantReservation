namespace RestaurantReservation.Domain.Models.Order;

public class DetailedOrderDto
{
    public Guid OrderId { get; set; }
    public Guid ReservationId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public ICollection<Entities.OrderItem> OrderItems { get; set; }
}