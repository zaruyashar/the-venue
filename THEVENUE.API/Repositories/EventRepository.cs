using Dapper;
using System.Data;
using THEVENUE.API.Data;
using THEVENUE.API.DTOs.Shared;
using THEVENUE.API.Models;

namespace THEVENUE.API.Repositories;

public class EventRepository : IEventRepository
{
    private readonly DapperContext _context;

    public EventRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Event>(
            "sp_GetAllEvents",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Event>(
            "sp_GetEventById",
            new { EventId = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Event>> GetByVenueAsync(int venueId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Event>(
            "sp_GetEventsByVenue",
            new { VenueId = venueId },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Event>> GetUpcomingPublicAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Event>(
            "sp_GetUpcomingPublicEvents",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EventLookupDto>> GetLookupsAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<EventLookupDto>(
            "sp_GetEventLookups",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(Event ev)
    {
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "sp_CreateEvent",
            new
            {
                ev.VenueId,
                ev.Title,
                ev.Description,
                ev.EventType,
                ev.StartDate,
                ev.EndDate,
                ev.ExpectedAttendees,
                ev.IsPublic
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateAsync(Event ev)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_UpdateEvent",
            new
            {
                ev.EventId,
                ev.VenueId,
                ev.Title,
                ev.Description,
                ev.EventType,
                ev.StartDate,
                ev.EndDate,
                ev.ExpectedAttendees,
                ev.IsPublic
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_DeleteEvent",
            new { EventId = id },
            commandType: CommandType.StoredProcedure);
    }
}