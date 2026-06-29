using THEVENUE.MVC.Models;

namespace THEVENUE.MVC.Services;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(int id);
    Task<int> CreateAsync(Contact contact);
    Task DeleteAsync(int id);
    Task MarkAsReadAsync(int id);
    Task<int> GetUnreadCountAsync();
}
