namespace THEVENUE.MVC.Models;

public class RevenueByVenue
{
    public string VenueName { get; set; } = string.Empty;
    public decimal TotalRevenue { get; set; }
    public int ReservationCount { get; set; }
}