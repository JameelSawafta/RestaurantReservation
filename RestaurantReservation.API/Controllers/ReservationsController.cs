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
[ApiVersion(1.0)]
[Authorize]
public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ReservationDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedReservations = await _reservationService.GetAllReservationsAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedReservations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto>> GetById(Guid id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationDto reservationDto)
    {
        var createdReservation = await _reservationService.CreateReservationAsync(reservationDto);
        return CreatedAtAction(nameof(GetById), new { id = createdReservation.ReservationId }, createdReservation);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReservationDto>> Update(Guid id, UpdateReservationDto reservationDto)
    {
        var updatedReservation = await _reservationService.UpdateReservationAsync(id, reservationDto);
        if (updatedReservation == null) return NotFound();
        return Ok(updatedReservation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _reservationService.DeleteReservationAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
    
    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<PaginatedList<ReservationDto>>> GetReservationsbycustomerId(Guid customerId,int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedManagers = await _reservationService.GetReservationsByCustomerIdAsync(customerId,pageNumber, pageSize, baseUrl);
        return Ok(paginatedManagers);
    }
    
    [HttpGet("{reservationId}/orders")]
    public async Task<ActionResult<PaginatedList<DetailedOrderDto>>> GetOrdersByReservationId(Guid reservationId, int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedOrders = await _reservationService.GetOrdersByReservationIdAsync(reservationId, pageNumber, pageSize, baseUrl);
        return Ok(paginatedOrders);
    }
}