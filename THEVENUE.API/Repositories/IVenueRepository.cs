using THEVENUE.API.DTOs.Shared;
using THEVENUE.API.Models;

namespace THEVENUE.API.Repositories;

public interface IVenueRepository
{
    Task<IEnumerable<Venue>> GetAllAsync(bool? isActive);
    Task<Venue?> GetByIdAsync(int id);
    Task<IEnumerable<Venue>> GetPublicVenuesAsync();
    Task<IEnumerable<VenueLookupDto>> GetLookupsAsync();
    Task<int> CreateAsync(Venue venue);
    Task UpdateAsync(Venue venue);
    Task DeleteAsync(int id);
}