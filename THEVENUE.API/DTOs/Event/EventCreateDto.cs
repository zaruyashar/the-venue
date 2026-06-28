using System.ComponentModel.DataAnnotations;

namespace THEVENUE.API.DTOs.Event;

public class EventCreateDto
{
    [Required]
    public int VenueId { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [MaxLength(80)]
    public string? EventType { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Range(1, 100000)]
    public int? ExpectedAttendees { get; set; }

    public bool IsPublic { get; set; } = true;
}