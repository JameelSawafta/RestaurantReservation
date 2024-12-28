using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Db.Repositories;

public class CustomerRepository : CRUDRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(RestaurantReservationDbContext context, PaginationService paginationService) : base(context, paginationService)
    {
    }
}