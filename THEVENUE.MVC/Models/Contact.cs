using System.ComponentModel.DataAnnotations;

namespace THEVENUE.MVC.Models;

public class Contact
{
    public int ContactId { get; set; }

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

    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}
