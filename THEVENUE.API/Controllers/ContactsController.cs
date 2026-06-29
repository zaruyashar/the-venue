using Microsoft.AspNetCore.Mvc;
using THEVENUE.API.DTOs.Contact;
using THEVENUE.API.Models;
using THEVENUE.API.Repositories;

namespace THEVENUE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository _repo;

    public ContactsController(IContactRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _repo.GetAllAsync();
        return Ok(contacts.Select(c => MapToResponse(c)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contact = await _repo.GetByIdAsync(id);
        if (contact is null) return NotFound();
        return Ok(MapToResponse(contact));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ContactCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var contact = new Contact
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Subject = dto.Subject,
            Message = dto.Message
        };

        var newId = await _repo.CreateAsync(contact);
        var created = await _repo.GetByIdAsync(newId);
        return CreatedAtAction(nameof(GetById), new { id = newId }, MapToResponse(created!));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        await _repo.DeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id:int}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        await _repo.MarkAsReadAsync(id);
        return NoContent();
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var count = await _repo.GetUnreadCountAsync();
        return Ok(new { Count = count });
    }

    private static ContactResponseDto MapToResponse(Contact contact)
    {
        return new ContactResponseDto
        {
            ContactId = contact.ContactId,
            Name = contact.Name,
            Email = contact.Email,
            Phone = contact.Phone,
            Subject = contact.Subject,
            Message = contact.Message,
            IsRead = contact.IsRead,
            CreatedAt = contact.CreatedAt
        };
    }
}
