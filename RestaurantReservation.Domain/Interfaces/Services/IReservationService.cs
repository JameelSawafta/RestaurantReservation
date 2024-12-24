using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;
using RestaurantReservation.Domain.Models.Reservation;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IReservationService
{
    Task<PaginatedList<ReservationDto>> GetAllReservationsAsync(int pageNumber, int pageSize, string baseUrl);
    Task<ReservationDto> GetReservationByIdAsync(Guid reservationId);
    Task<ReservationDto> CreateReservationAsync(ReservationDto reservationDto);
    Task<ReservationDto> UpdateReservationAsync(Guid reservationId, ReservationDto reservationDto);
    Task<bool> DeleteReservationAsync(Guid reservationId);
    
    Task<PaginatedList<ReservationDto>> GetReservationsByCustomerIdAsync(Guid customerId,int pageNumber, int pageSize, string baseUrl);
    Task<PaginatedList<DetailedOrderDto>> GetOrdersByReservationIdAsync(Guid reservationId, int pageNumber, int pageSize, string baseUrl);
}