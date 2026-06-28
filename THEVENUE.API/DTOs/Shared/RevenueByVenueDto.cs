namespace THEVENUE.API.DTOs.Shared
{
    public class RevenueByVenueDto
    {
        public string VenueName { get; set; } = string.Empty;
        public decimal TotalRevenue { get; set; }
        public int ReservationCount { get; set; }
    }
}
