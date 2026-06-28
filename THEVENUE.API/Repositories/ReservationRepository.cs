using Dapper;
using System.Data;
using THEVENUE.API.Data;
using THEVENUE.API.DTOs.Shared;
using THEVENUE.API.Models;

namespace THEVENUE.API.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly DapperContext _context;

    public ReservationRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Reservation>(
            "sp_GetAllReservations",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Reservation>(
            "sp_GetReservationById",
            new { ReservationId = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Reservation>> GetByVenueAsync(int venueId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Reservation>(
            "sp_GetReservationsByVenue",
            new { VenueId = venueId },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Reservation>> GetByStatusAsync(string status)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Reservation>(
            "sp_GetReservationsByStatus",
            new { Status = status },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(Reservation reservation)
    {
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "sp_CreateReservation",
            new
            {
                reservation.VenueId,
                reservation.EventId,
                reservation.GuestName,
                reservation.GuestEmail,
                reservation.GuestPhone,
                reservation.GuestCount,
                reservation.Status,
                reservation.TotalAmount,
                reservation.Notes
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_UpdateReservation",
            new
            {
                reservation.ReservationId,
                reservation.VenueId,
                reservation.EventId,
                reservation.GuestName,
                reservation.GuestEmail,
                reservation.GuestPhone,
                reservation.GuestCount,
                reservation.Status,
                reservation.TotalAmount,
                reservation.Notes
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateStatusAsync(int id, string status)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_UpdateReservationStatus",
            new { ReservationId = id, Status = status },
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_DeleteReservation",
            new { ReservationId = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstAsync<DashboardStatsDto>(
            "sp_GetDashboardStats",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RevenueByVenueDto>> GetRevenueByVenueAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<RevenueByVenueDto>(
            "sp_GetRevenueByVenue",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ReservationsByMonthDto>> GetReservationsByMonthAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<ReservationsByMonthDto>(
            "sp_GetReservationsByMonth",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EventTypeBreakdownDto>> GetEventTypeBreakdownAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<EventTypeBreakdownDto>(
            "sp_GetEventTypeBreakdown",
            commandType: CommandType.StoredProcedure);
    }
}