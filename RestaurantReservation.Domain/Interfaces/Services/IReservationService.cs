using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;
using RestaurantReservation.Domain.Models.Reservation;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IReservationService : ICRUDService<ReservationDto,CreateAndUpdateReservationDto>
{
    Task<PaginatedList<ReservationDto>> GetReservationsByCustomerIdAsync(Guid customerId,int pageNumber, int pageSize);
    Task<PaginatedList<DetailedOrderDto>> GetOrdersByReservationIdAsync(Guid reservationId, int pageNumber, int pageSize);
}