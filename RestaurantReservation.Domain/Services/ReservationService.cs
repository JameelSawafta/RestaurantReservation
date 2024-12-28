using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;
using RestaurantReservation.Domain.Models.Reservation;

namespace RestaurantReservation.Domain.Services;

public class ReservationService : CRUDService<Reservation,ReservationDto,CreateAndUpdateReservationDto>,  IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(ICRUDRepository<Reservation> repository, IMapper mapper, IReservationRepository reservationRepository) : base(repository, mapper)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<PaginatedList<ReservationDto>> GetReservationsByCustomerIdAsync(Guid customerId,int pageNumber, int pageSize)
    {
        var (reservations, totalItemCount) = await _reservationRepository.GetReservationsByCustomerIdAsync(customerId, pageNumber, pageSize);
        if (totalItemCount == 0)
        {
            throw new KeyNotFoundException($"{nameof(Reservation)} with not data found.");
        }

        var pageData = new PageData(totalItemCount, pageSize, pageNumber);
        var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return new PaginatedList<ReservationDto>(reservationDtos.ToList(), pageData);
        
    }
    
    public async Task<PaginatedList<DetailedOrderDto>> GetOrdersByReservationIdAsync(Guid reservationId, int pageNumber, int pageSize)
    {
        var (orders, totalItemCount) = await _reservationRepository.GetOrdersByReservationIdAsync(reservationId, pageNumber, pageSize);
        if (totalItemCount == 0)
        {
            throw new KeyNotFoundException($"{nameof(Order)} with not data found.");
        }

        var pageData = new PageData(totalItemCount, pageSize, pageNumber);
        var orderDtos = _mapper.Map<IEnumerable<DetailedOrderDto>>(orders);

        return new PaginatedList<DetailedOrderDto>(orderDtos.ToList(), pageData);
    }
}