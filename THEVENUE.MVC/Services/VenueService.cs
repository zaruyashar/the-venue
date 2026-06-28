using Newtonsoft.Json;
using THEVENUE.MVC.Models;

namespace THEVENUE.MVC.Services;

public class VenueService : IVenueService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public VenueService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("TheVenueApi");

    public async Task<IEnumerable<Venue>> GetAllAsync(bool? isActive = null)
    {
        var url = isActive.HasValue ? $"api/venues?isActive={isActive.Value}" : "api/venues";
        var response = await Client.GetStringAsync(url);
        return JsonConvert.DeserializeObject<IEnumerable<Venue>>(response) ?? [];
    }

    public async Task<Venue?> GetByIdAsync(int id)
    {
        var response = await Client.GetAsync($"api/venues/{id}");
        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Venue>(json);
    }

    public async Task<IEnumerable<Venue>> GetPublicVenuesAsync()
    {
        var response = await Client.GetStringAsync("api/venues/public");
        return JsonConvert.DeserializeObject<IEnumerable<Venue>>(response) ?? [];
    }

    public async Task<IEnumerable<VenueLookup>> GetLookupsAsync()
    {
        var response = await Client.GetStringAsync("api/venues/lookups");
        return JsonConvert.DeserializeObject<IEnumerable<VenueLookup>>(response) ?? [];
    }

    public async Task<int> CreateAsync(Venue venue)
    {
        var response = await Client.PostAsJsonAsync("api/venues", venue);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var created = JsonConvert.DeserializeObject<Venue>(json);
        return created!.VenueId;
    }

    public async Task UpdateAsync(Venue venue)
    {
        var response = await Client.PutAsJsonAsync($"api/venues/{venue.VenueId}", venue);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await Client.DeleteAsync($"api/venues/{id}");
        response.EnsureSuccessStatusCode();
    }
}