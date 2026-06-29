using System.ComponentModel.DataAnnotations;

namespace THEVENUE.API.DTOs.Contact;

public class ContactCreateDto
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Subject { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;
}
