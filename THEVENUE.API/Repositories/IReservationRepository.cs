using THEVENUE.API.DTOs.Shared;
using THEVENUE.API.Models;

namespace THEVENUE.API.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<IEnumerable<Reservation>> GetByVenueAsync(int venueId);
    Task<IEnumerable<Reservation>> GetByStatusAsync(string status);
    Task<int> CreateAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task UpdateStatusAsync(int id, string status);
    Task DeleteAsync(int id);
    Task<DashboardStatsDto> GetDashboardStatsAsync();
    Task<IEnumerable<RevenueByVenueDto>> GetRevenueByVenueAsync();
    Task<IEnumerable<ReservationsByMonthDto>> GetReservationsByMonthAsync();
    Task<IEnumerable<EventTypeBreakdownDto>> GetEventTypeBreakdownAsync();
}