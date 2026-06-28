using THEVENUE.MVC.Models;

namespace THEVENUE.MVC.Services;

public interface IEventService
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task<IEnumerable<Event>> GetByVenueAsync(int venueId);
    Task<IEnumerable<Event>> GetUpcomingPublicAsync();
    Task<IEnumerable<EventLookup>> GetLookupsAsync();
    Task<int> CreateAsync(Event ev);
    Task UpdateAsync(Event ev);
    Task DeleteAsync(int id);
}