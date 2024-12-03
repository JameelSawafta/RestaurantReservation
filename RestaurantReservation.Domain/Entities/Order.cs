using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid OrderId { get; set; }
    
    public Guid ReservationId { get; set; }
    
    public Guid EmployeeId { get; set; }
    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime OrderDate { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    [ForeignKey("ReservationId")]
    public Reservation Reservation { get; set; }
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}