using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Entities;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public Guid ReservationId { get; set; } 
    public Guid CustomerId { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid TableId { get; set; }
    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime ReservationDate { get; set; }
    [Required]
    [Range(0,int.MaxValue)]
    public int PartySize { get; set; } 
   
    [ForeignKey("CustomerId")] 
    public Customer Customer { get; set; }
    [ForeignKey("RestaurantId")] 
    public Restaurant Restaurant { get; set; }
    [ForeignKey("TableId")] 
    public Table Table { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}