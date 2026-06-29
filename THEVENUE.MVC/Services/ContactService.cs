using Newtonsoft.Json;
using THEVENUE.MVC.Models;

namespace THEVENUE.MVC.Services;

public class ContactService : IContactService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ContactService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("TheVenueApi");

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        var response = await Client.GetStringAsync("api/contacts");
        return JsonConvert.DeserializeObject<IEnumerable<Contact>>(response) ?? [];
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        var response = await Client.GetAsync($"api/contacts/{id}");
        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Contact>(json);
    }

    public async Task<int> CreateAsync(Contact contact)
    {
        var response = await Client.PostAsJsonAsync("api/contacts", contact);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var created = JsonConvert.DeserializeObject<Contact>(json);
        return created!.ContactId;
    }

    public async Task DeleteAsync(int id)
    {
        var response = await Client.DeleteAsync($"api/contacts/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task MarkAsReadAsync(int id)
    {
        var response = await Client.PatchAsJsonAsync($"api/contacts/{id}/read", new { });
        response.EnsureSuccessStatusCode();
    }

    public async Task<int> GetUnreadCountAsync()
    {
        var response = await Client.GetStringAsync("api/contacts/unread-count");
        var result = JsonConvert.DeserializeAnonymousType(response, new { Count = 0 });
        return result?.Count ?? 0;
    }
}
