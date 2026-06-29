using System.Data;
using Dapper;
using THEVENUE.API.Data;
using THEVENUE.API.Models;

namespace THEVENUE.API.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly DapperContext _context;

    public ContactRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Contact>(
            "sp_GetAllContacts",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Contact>(
            "sp_GetContactById",
            new { ContactId = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CreateAsync(Contact contact)
    {
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "sp_CreateContact",
            new
            {
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Subject = contact.Subject,
                Message = contact.Message
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_DeleteContact",
            new { ContactId = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task MarkAsReadAsync(int id)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_MarkContactAsRead",
            new { ContactId = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> GetUnreadCountAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "sp_GetUnreadContactCount",
            commandType: CommandType.StoredProcedure);
    }
}
