using Newtonsoft.Json;
using THEVENUE.MVC.Models;

namespace THEVENUE.MVC.Services;

public class ReservationService : IReservationService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ReservationService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("TheVenueApi");

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        var response = await Client.GetStringAsync("api/reservations");
        return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(response) ?? [];
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        var response = await Client.GetAsync($"api/reservations/{id}");
        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Reservation>(json);
    }

    public async Task<IEnumerable<Reservation>> GetByVenueAsync(int venueId)
    {
        var response = await Client.GetStringAsync($"api/reservations/by-venue/{venueId}");
        return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(response) ?? [];
    }

    public async Task<IEnumerable<Reservation>> GetByStatusAsync(string status)
    {
        var response = await Client.GetStringAsync($"api/reservations/by-status/{status}");
        return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(response) ?? [];
    }

    public async Task<int> CreateAsync(Reservation reservation)
    {
        var response = await Client.PostAsJsonAsync("api/reservations", reservation);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var created = JsonConvert.DeserializeObject<Reservation>(json);
        return created!.ReservationId;
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        var response = await Client.PutAsJsonAsync($"api/reservations/{reservation.ReservationId}", reservation);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateStatusAsync(int id, string status)
    {
        var response = await Client.PatchAsJsonAsync($"api/reservations/{id}/status", new { Status = status });
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await Client.DeleteAsync($"api/reservations/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<DashboardStats> GetDashboardStatsAsync()
    {
        var response = await Client.GetStringAsync("api/reservations/dashboard-stats");
        return JsonConvert.DeserializeObject<DashboardStats>(response)!;
    }

    public async Task<IEnumerable<RevenueByVenue>> GetRevenueByVenueAsync()
    {
        var response = await Client.GetStringAsync("api/reservations/revenue-by-venue");
        return JsonConvert.DeserializeObject<IEnumerable<RevenueByVenue>>(response) ?? [];
    }

    public async Task<IEnumerable<ReservationsByMonth>> GetReservationsByMonthAsync()
    {
        var response = await Client.GetStringAsync("api/reservations/by-month");
        return JsonConvert.DeserializeObject<IEnumerable<ReservationsByMonth>>(response) ?? [];
    }

    public async Task<IEnumerable<EventTypeBreakdown>> GetEventTypeBreakdownAsync()
    {
        var response = await Client.GetStringAsync("api/reservations/event-type-breakdown");
        return JsonConvert.DeserializeObject<IEnumerable<EventTypeBreakdown>>(response) ?? [];
    }
}