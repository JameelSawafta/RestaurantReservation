using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;
using RestaurantReservation.Domain.Models.Reservation;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/reservation")]
[ApiVersion("1.0")]
[Authorize]
public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<PaginatedList<ReservationDto>> GetAll(int pageNumber, int pageSize)
    {
       var paginatedReservations = await _reservationService.GetAllAsync(pageNumber, pageSize);
        return paginatedReservations;
    }

    [HttpGet("{id}")]
    public async Task<ReservationDto> GetById(Guid id)
    {
        var reservation = await _reservationService.GetByIdAsync(id);
        return reservation;
    }

    [HttpPost]
    public async Task<ReservationDto> Create(CreateAndUpdateReservationDto reservationDto)
    {
        var createdReservation = await _reservationService.CreateAsync(reservationDto);
        return createdReservation;
    }

    [HttpPut("{id}")]
    public async Task<ReservationDto> Update(Guid id, CreateAndUpdateReservationDto reservationDto)
    {
        var updatedReservation = await _reservationService.UpdateAsync(id, reservationDto);
        return updatedReservation;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _reservationService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }   
    
    [HttpGet("customer/{customerId}")]
    public async Task<PaginatedList<ReservationDto>> GetReservationsbycustomerId(Guid customerId,int pageNumber, int pageSize)
    {
        var paginatedReservation = await _reservationService.GetReservationsByCustomerIdAsync(customerId,pageNumber, pageSize);
        return paginatedReservation;
    }
    
    [HttpGet("{reservationId}/orders")]
    public async Task<PaginatedList<DetailedOrderDto>> GetOrdersByReservationId(Guid reservationId, int pageNumber, int pageSize)
    {
        var paginatedOrders = await _reservationService.GetOrdersByReservationIdAsync(reservationId, pageNumber, pageSize);
        return paginatedOrders;
    }
}