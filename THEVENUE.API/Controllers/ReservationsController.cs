using Microsoft.AspNetCore.Mvc;
using THEVENUE.API.DTOs.Reservation;
using THEVENUE.API.Models;
using THEVENUE.API.Repositories;

namespace THEVENUE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationRepository _repo;

    public ReservationsController(IReservationRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reservations = await _repo.GetAllAsync();
        return Ok(reservations.Select(r => MapToResponse(r)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reservation = await _repo.GetByIdAsync(id);
        if (reservation is null) return NotFound();
        return Ok(MapToResponse(reservation));
    }

    [HttpGet("by-venue/{venueId:int}")]
    public async Task<IActionResult> GetByVenue(int venueId)
    {
        var reservations = await _repo.GetByVenueAsync(venueId);
        return Ok(reservations.Select(r => MapToResponse(r)));
    }

    [HttpGet("by-status/{status}")]
    public async Task<IActionResult> GetByStatus(string status)
    {
        var reservations = await _repo.GetByStatusAsync(status);
        return Ok(reservations.Select(r => MapToResponse(r)));
    }

    [HttpGet("dashboard-stats")]
    public async Task<IActionResult> GetDashboardStats()
    {
        var stats = await _repo.GetDashboardStatsAsync();
        return Ok(stats);
    }

    [HttpGet("revenue-by-venue")]
    public async Task<IActionResult> GetRevenueByVenue()
    {
        var data = await _repo.GetRevenueByVenueAsync();
        return Ok(data);
    }

    [HttpGet("by-month")]
    public async Task<IActionResult> GetByMonth()
    {
        var data = await _repo.GetReservationsByMonthAsync();
        return Ok(data);
    }

    [HttpGet("event-type-breakdown")]
    public async Task<IActionResult> GetEventTypeBreakdown()
    {
        var data = await _repo.GetEventTypeBreakdownAsync();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReservationCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var reservation = new Reservation
        {
            VenueId = dto.VenueId,
            EventId = dto.EventId,
            GuestName = dto.GuestName,
            GuestEmail = dto.GuestEmail,
            GuestPhone = dto.GuestPhone,
            GuestCount = dto.GuestCount,
            Status = dto.Status,
            TotalAmount = dto.TotalAmount,
            Notes = dto.Notes
        };

        var newId = await _repo.CreateAsync(reservation);
        var created = await _repo.GetByIdAsync(newId);
        return CreatedAtAction(nameof(GetById), new { id = newId }, MapToResponse(created!));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReservationUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != dto.ReservationId) return BadRequest("ID mismatch.");

        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        var reservation = new Reservation
        {
            ReservationId = dto.ReservationId,
            VenueId = dto.VenueId,
            EventId = dto.EventId,
            GuestName = dto.GuestName,
            GuestEmail = dto.GuestEmail,
            GuestPhone = dto.GuestPhone,
            GuestCount = dto.GuestCount,
            Status = dto.Status,
            TotalAmount = dto.TotalAmount,
            Notes = dto.Notes
        };

        await _repo.UpdateAsync(reservation);
        return NoContent();
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] ReservationStatusDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        await _repo.UpdateStatusAsync(id, dto.Status);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }

    private static ReservationResponseDto MapToResponse(Reservation r) => new()
    {
        ReservationId = r.ReservationId,
        VenueId = r.VenueId,
        VenueName = r.VenueName,
        EventId = r.EventId,
        EventTitle = r.EventTitle,
        GuestName = r.GuestName,
        GuestEmail = r.GuestEmail,
        GuestPhone = r.GuestPhone,
        GuestCount = r.GuestCount,
        Status = r.Status,
        TotalAmount = r.TotalAmount,
        Notes = r.Notes,
        CreatedAt = r.CreatedAt
    };
}