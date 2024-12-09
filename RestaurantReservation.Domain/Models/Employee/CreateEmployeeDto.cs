using RestaurantReservation.Domain.Enums;

namespace RestaurantReservation.Domain.Models.Employee;

public class CreateEmployeeDto
{
    public Guid RestaurantId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EmployeePosition Position { get; set; }
}