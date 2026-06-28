using Microsoft.AspNetCore.Mvc;
using THEVENUE.API.DTOs.Event;
using THEVENUE.API.Models;
using THEVENUE.API.Repositories;

namespace THEVENUE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventRepository _repo;

    public EventsController(IEventRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _repo.GetAllAsync();
        return Ok(events.Select(e => MapToResponse(e)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ev = await _repo.GetByIdAsync(id);
        if (ev is null) return NotFound();
        return Ok(MapToResponse(ev));
    }

    [HttpGet("by-venue/{venueId:int}")]
    public async Task<IActionResult> GetByVenue(int venueId)
    {
        var events = await _repo.GetByVenueAsync(venueId);
        return Ok(events.Select(e => MapToResponse(e)));
    }

    [HttpGet("upcoming-public")]
    public async Task<IActionResult> GetUpcomingPublic()
    {
        var events = await _repo.GetUpcomingPublicAsync();
        return Ok(events.Select(e => MapToResponse(e)));
    }

    [HttpGet("lookups")]
    public async Task<IActionResult> GetLookups()
    {
        var lookups = await _repo.GetLookupsAsync();
        return Ok(lookups);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EventCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var ev = new Event
        {
            VenueId = dto.VenueId,
            Title = dto.Title,
            Description = dto.Description,
            EventType = dto.EventType,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            ExpectedAttendees = dto.ExpectedAttendees,
            IsPublic = dto.IsPublic
        };

        var newId = await _repo.CreateAsync(ev);
        var created = await _repo.GetByIdAsync(newId);
        return CreatedAtAction(nameof(GetById), new { id = newId }, MapToResponse(created!));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] EventUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != dto.EventId) return BadRequest("ID mismatch.");

        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        var ev = new Event
        {
            EventId = dto.EventId,
            VenueId = dto.VenueId,
            Title = dto.Title,
            Description = dto.Description,
            EventType = dto.EventType,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            ExpectedAttendees = dto.ExpectedAttendees,
            IsPublic = dto.IsPublic
        };

        await _repo.UpdateAsync(ev);
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

    private static EventResponseDto MapToResponse(Event e) => new()
    {
        EventId = e.EventId,
        VenueId = e.VenueId,
        VenueName = e.VenueName,
        Title = e.Title,
        Description = e.Description,
        EventType = e.EventType,
        StartDate = e.StartDate,
        EndDate = e.EndDate,
        ExpectedAttendees = e.ExpectedAttendees,
        IsPublic = e.IsPublic,
        CreatedAt = e.CreatedAt
    };
}