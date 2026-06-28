using System.ComponentModel.DataAnnotations;

namespace THEVENUE.API.DTOs.Reservation;

public class ReservationUpdateDto
{
    [Required]
    public int ReservationId { get; set; }

    [Required]
    public int VenueId { get; set; }

    [Required]
    public int EventId { get; set; }

    [Required, MaxLength(150)]
    public string GuestName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string GuestEmail { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? GuestPhone { get; set; }

    [Required, Range(1, 10000)]
    public int GuestCount { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    [Range(0, 9999999.99)]
    public decimal TotalAmount { get; set; }

    public string? Notes { get; set; }
}