using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class CustomerRepository : CRUDRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(RestaurantReservationDbContext context) : base(context)
    {
        
    }
    
}