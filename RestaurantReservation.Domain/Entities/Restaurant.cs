using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Entities;

public class Restaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public Guid RestaurantId { get; set; }
    [Required]
    [MaxLength(100)] 
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Address { get; set; }
    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; }
    [MaxLength(50)]
    public string OpeningHours { get; set; }
    
    public ICollection<Employee> Employees { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; }
    public ICollection<Table> Tables { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}