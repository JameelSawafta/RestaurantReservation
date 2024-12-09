using RestaurantReservation.Domain.Entities;

namespace RestaurantReservation.Domain.Interfaces.Repositories;

public interface IReservationRepository : ICRUDRepository<Reservation>
{
    Task<(IEnumerable<Reservation>, int TotalCount)> GetReservationsByCustomerIdAsync(Guid customerId,int pageNumber, int pageSize);
    Task<(IEnumerable<Order>, int TotalCount)> GetOrdersByReservationIdAsync(Guid reservationId, int pageNumber, int pageSize);
    
}   