using THEVENUE.MVC.Models;

namespace THEVENUE.MVC.Services;

public interface IVenueService
{
    Task<IEnumerable<Venue>> GetAllAsync(bool? isActive = null);
    Task<Venue?> GetByIdAsync(int id);
    Task<IEnumerable<Venue>> GetPublicVenuesAsync();
    Task<IEnumerable<VenueLookup>> GetLookupsAsync();
    Task<int> CreateAsync(Venue venue);
    Task UpdateAsync(Venue venue);
    Task DeleteAsync(int id);
}