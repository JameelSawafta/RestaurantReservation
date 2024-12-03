using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Entities;

public class Table
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public Guid TableId { get; set; } 
    public Guid RestaurantId { get; set; }
    [Required]
    [Range(0,int.MaxValue)]
    public int Capacity { get; set; }
    
    [ForeignKey("RestaurantId")]
    public Restaurant Restaurant { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}