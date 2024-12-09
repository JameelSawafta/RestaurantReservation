using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservation.Domain.Enums;

namespace RestaurantReservation.Domain.Entities;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid EmployeeId { get; set; }
    public Guid RestaurantId { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    public EmployeePosition Position { get; set; }
    
    [ForeignKey("RestaurantId")]
    public Restaurant Restaurant { get; set; }
    public ICollection<Order> Orders { get; set; }
}