using Dapper;
using System.Data;
using THEVENUE.API.Data;
using THEVENUE.API.DTOs.Shared;
using THEVENUE.API.Models;

namespace THEVENUE.API.Repositories;

public class VenueRepository : IVenueRepository
{
    private readonly DapperContext _context;

    public VenueRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Venue>> GetAllAsync(bool? isActive)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Venue>(
            "sp_GetAllVenues",
            new { IsActive = isActive },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Venue?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Venue>(
            "sp_GetVenueById",
            new { VenueId = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Venue>> GetPublicVenuesAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Venue>(
            "sp_GetPublicVenues",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<VenueLookupDto>> GetLookupsAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<VenueLookupDto>(
            "sp_GetVenueLookups",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(Venue venue)
    {
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteScalarAsync<int>(
            "sp_CreateVenue",
            new
            {
                venue.Name,
                venue.Description,
                venue.Capacity,
                venue.PricePerHour,
                venue.Location,
                venue.ImageUrl,
                venue.IsActive
            },
            commandType: CommandType.StoredProcedure);
        return result;
    }

    public async Task UpdateAsync(Venue venue)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_UpdateVenue",
            new
            {
                venue.VenueId,
                venue.Name,
                venue.Description,
                venue.Capacity,
                venue.PricePerHour,
                venue.Location,
                venue.ImageUrl,
                venue.IsActive
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_DeleteVenue",
            new { VenueId = id },
            commandType: CommandType.StoredProcedure);
    }
}