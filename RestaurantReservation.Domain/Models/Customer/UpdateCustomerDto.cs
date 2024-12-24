namespace RestaurantReservation.Domain.Models.Customer;

public class UpdateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
// same as CreateCustomerDto you can use customer dto for both create and update
/*
public class CreateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}*/