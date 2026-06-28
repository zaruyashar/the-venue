using Newtonsoft.Json;
using THEVENUE.MVC.Models;

namespace THEVENUE.MVC.Services;

public class EventService : IEventService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EventService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("TheVenueApi");

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        var response = await Client.GetStringAsync("api/events");
        return JsonConvert.DeserializeObject<IEnumerable<Event>>(response) ?? [];
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        var response = await Client.GetAsync($"api/events/{id}");
        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Event>(json);
    }

    public async Task<IEnumerable<Event>> GetByVenueAsync(int venueId)
    {
        var response = await Client.GetStringAsync($"api/events/by-venue/{venueId}");
        return JsonConvert.DeserializeObject<IEnumerable<Event>>(response) ?? [];
    }

    public async Task<IEnumerable<Event>> GetUpcomingPublicAsync()
    {
        var response = await Client.GetStringAsync("api/events/upcoming-public");
        return JsonConvert.DeserializeObject<IEnumerable<Event>>(response) ?? [];
    }

    public async Task<IEnumerable<EventLookup>> GetLookupsAsync()
    {
        var response = await Client.GetStringAsync("api/events/lookups");
        return JsonConvert.DeserializeObject<IEnumerable<EventLookup>>(response) ?? [];
    }

    public async Task<int> CreateAsync(Event ev)
    {
        var response = await Client.PostAsJsonAsync("api/events", ev);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var created = JsonConvert.DeserializeObject<Event>(json);
        return created!.EventId;
    }

    public async Task UpdateAsync(Event ev)
    {
        var response = await Client.PutAsJsonAsync($"api/events/{ev.EventId}", ev);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await Client.DeleteAsync($"api/events/{id}");
        response.EnsureSuccessStatusCode();
    }
}