using THEVENUE.MVC.Models;

namespace THEVENUE.MVC.Services;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<IEnumerable<Reservation>> GetByVenueAsync(int venueId);
    Task<IEnumerable<Reservation>> GetByStatusAsync(string status);
    Task<int> CreateAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task UpdateStatusAsync(int id, string status);
    Task DeleteAsync(int id);
    Task<DashboardStats> GetDashboardStatsAsync();
    Task<IEnumerable<RevenueByVenue>> GetRevenueByVenueAsync();
    Task<IEnumerable<ReservationsByMonth>> GetReservationsByMonthAsync();
    Task<IEnumerable<EventTypeBreakdown>> GetEventTypeBreakdownAsync();
}