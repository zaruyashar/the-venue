using System.ComponentModel.DataAnnotations;

namespace THEVENUE.API.DTOs.Reservation;

public class ReservationStatusDto
{
    [Required]
    public string Status { get; set; } = string.Empty;
}