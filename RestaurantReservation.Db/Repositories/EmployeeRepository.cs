using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository : CRUDRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RestaurantReservationDbContext context) : base(context)
    {
    }
}