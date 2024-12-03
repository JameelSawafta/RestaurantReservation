using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Domain.Entities;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid OrderItemId { get; set; } 
    
    public Guid OrderId { get; set; }
    
    public Guid ItemId { get; set; }
    [Required]
    [Range(0,int.MaxValue)]
    public int Quantity { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    [ForeignKey("ItemId")]
    public MenuItem MenuItem { get; set; }
}