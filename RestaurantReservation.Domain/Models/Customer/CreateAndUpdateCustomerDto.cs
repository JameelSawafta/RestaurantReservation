namespace RestaurantReservation.Domain.Models.Customer;

public class CreateAndUpdateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}