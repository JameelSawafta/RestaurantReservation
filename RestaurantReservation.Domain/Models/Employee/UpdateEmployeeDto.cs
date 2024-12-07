namespace RestaurantReservation.Domain.Models.Employee;

public class UpdateEmployeeDto
{
    public Guid RestaurantId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
}