namespace RestaurantReservation.Domain.Models.Reservation;

public class ReservationDto
{
    public Guid ReservationId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid TableId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
}