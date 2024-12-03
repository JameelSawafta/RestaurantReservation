using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Entities;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CustomerId { get; set; }  
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [MaxLength(100)]
    public string Email { get; set; }
    [MaxLength(15)]
    public string PhoneNumber { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; }
}