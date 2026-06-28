using System.ComponentModel.DataAnnotations;

namespace THEVENUE.API.DTOs.Venue;

public class VenueCreateDto
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required, Range(1, 10000)]
    public int Capacity { get; set; }

    [Required, Range(0.01, 999999.99)]
    public decimal PricePerHour { get; set; }

    [Required, MaxLength(250)]
    public string Location { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;
}