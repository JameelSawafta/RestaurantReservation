using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Entities;

public class MenuItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ItemId { get; set; }  
    public Guid RestaurantId { get; set; }
    [Required]
    [MaxLength(100)] 
    public string Name { get; set; }
    [Required]
    [MaxLength(200)] 
    public string Description { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [ForeignKey("RestaurantId")]
    public Restaurant Restaurant { get; set; }
}