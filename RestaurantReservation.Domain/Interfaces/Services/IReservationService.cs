using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Reservation;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface IReservationService
{
    Task<PaginatedList<ReservationDto>> GetAllReservationsAsync(int pageNumber, int pageSize, string baseUrl);
    Task<ReservationDto> GetReservationByIdAsync(Guid reservationId);
    Task<ReservationDto> CreateReservationAsync(CreateReservationDto reservationDto);
    Task<ReservationDto> UpdateReservationAsync(Guid reservationId, UpdateReservationDto reservationDto);
    Task<bool> DeleteReservationAsync(Guid reservationId);
    
    Task<PaginatedList<ReservationDto>> GetReservationsByCustomerIdAsync(Guid customerId,int pageNumber, int pageSize, string baseUrl);
}