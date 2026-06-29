using THEVENUE.API.Models;

namespace THEVENUE.API.Repositories;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(int id);
    Task<int> CreateAsync(Contact contact);
    Task DeleteAsync(int id);
    Task MarkAsReadAsync(int id);
    Task<int> GetUnreadCountAsync();
}
