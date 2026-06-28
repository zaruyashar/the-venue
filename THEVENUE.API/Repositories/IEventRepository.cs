using THEVENUE.API.DTOs.Shared;
using THEVENUE.API.Models;

namespace THEVENUE.API.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task<IEnumerable<Event>> GetByVenueAsync(int venueId);
    Task<IEnumerable<Event>> GetUpcomingPublicAsync();
    Task<IEnumerable<EventLookupDto>> GetLookupsAsync();
    Task<int> CreateAsync(Event ev);
    Task UpdateAsync(Event ev);
    Task DeleteAsync(int id);
}