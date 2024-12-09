using RestaurantReservation.Domain.Enums;

namespace RestaurantReservation.Domain.Models.Employee;

public class EmployeeDto
{
    public Guid EmployeeId { get; set; }
    public Guid RestaurantId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EmployeePosition Position { get; set; }
}