using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;
using RestaurantReservation.Domain.Models.Reservation;

namespace RestaurantReservation.Domain.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ReservationDto>> GetAllReservationsAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (reservations, totalItemCount) = await _reservationRepository.GetAllAsync(pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return new PaginatedList<ReservationDto>(reservationDtos.ToList(), pageData);
    }

    public async Task<ReservationDto> GetReservationByIdAsync(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId);
        return reservation == null ? null : _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);
        var createdReservation = await _reservationRepository.CreateAsync(reservation);
        return _mapper.Map<ReservationDto>(createdReservation);
    }

    public async Task<ReservationDto> UpdateReservationAsync(Guid reservationId, UpdateReservationDto reservationDto)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId);
        if (reservation == null) return null;

        _mapper.Map(reservationDto, reservation);
        var updatedReservation = await _reservationRepository.UpdateAsync(reservation);
        return _mapper.Map<ReservationDto>(updatedReservation);
    }

    public async Task<bool> DeleteReservationAsync(Guid reservationId)
    {
        return await _reservationRepository.DeleteAsync(reservationId);
    }
    
    public async Task<PaginatedList<ReservationDto>> GetReservationsByCustomerIdAsync(Guid customerId,int pageNumber, int pageSize, string baseUrl)
    {
        var (reservations, totalItemCount) = await _reservationRepository.GetReservationsByCustomerIdAsync(customerId, pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return new PaginatedList<ReservationDto>(reservationDtos.ToList(), pageData);
        
    }
    
    public async Task<PaginatedList<DetailedOrderDto>> GetOrdersByReservationIdAsync(Guid reservationId, int pageNumber, int pageSize, string baseUrl)
    {
        var (orders, totalItemCount) = await _reservationRepository.GetOrdersByReservationIdAsync(reservationId, pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var orderDtos = _mapper.Map<IEnumerable<DetailedOrderDto>>(orders);

        return new PaginatedList<DetailedOrderDto>(orderDtos.ToList(), pageData);
    }
}