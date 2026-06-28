using Microsoft.AspNetCore.Mvc;
using THEVENUE.API.DTOs.Venue;
using THEVENUE.API.Models;
using THEVENUE.API.Repositories;

namespace THEVENUE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VenuesController : ControllerBase
{
    private readonly IVenueRepository _repo;

    public VenuesController(IVenueRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? isActive)
    {
        var venues = await _repo.GetAllAsync(isActive);
        var result = venues.Select(v => MapToResponse(v));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var venue = await _repo.GetByIdAsync(id);
        if (venue is null) return NotFound();
        return Ok(MapToResponse(venue));
    }

    [HttpGet("public")]
    public async Task<IActionResult> GetPublic()
    {
        var venues = await _repo.GetPublicVenuesAsync();
        var result = venues.Select(v => MapToResponse(v));
        return Ok(result);
    }

    [HttpGet("lookups")]
    public async Task<IActionResult> GetLookups()
    {
        var lookups = await _repo.GetLookupsAsync();
        return Ok(lookups);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VenueCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var venue = new Venue
        {
            Name = dto.Name,
            Description = dto.Description,
            Capacity = dto.Capacity,
            PricePerHour = dto.PricePerHour,
            Location = dto.Location,
            ImageUrl = dto.ImageUrl,
            IsActive = dto.IsActive
        };

        var newId = await _repo.CreateAsync(venue);
        var created = await _repo.GetByIdAsync(newId);
        return CreatedAtAction(nameof(GetById), new { id = newId }, MapToResponse(created!));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] VenueUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != dto.VenueId) return BadRequest("ID mismatch.");

        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        var venue = new Venue
        {
            VenueId = dto.VenueId,
            Name = dto.Name,
            Description = dto.Description,
            Capacity = dto.Capacity,
            PricePerHour = dto.PricePerHour,
            Location = dto.Location,
            ImageUrl = dto.ImageUrl,
            IsActive = dto.IsActive
        };

        await _repo.UpdateAsync(venue);
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

    private static VenueResponseDto MapToResponse(Venue v) => new()
    {
        VenueId = v.VenueId,
        Name = v.Name,
        Description = v.Description,
        Capacity = v.Capacity,
        PricePerHour = v.PricePerHour,
        Location = v.Location,
        ImageUrl = v.ImageUrl,
        IsActive = v.IsActive,
        CreatedAt = v.CreatedAt
    };
}