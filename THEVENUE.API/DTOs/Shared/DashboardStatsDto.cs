namespace THEVENUE.API.DTOs.Shared
{
    public class DashboardStatsDto
    {
        public int TotalVenues { get; set; }
        public int TotalEvents { get; set; }
        public int TotalReservations { get; set; }
        public int PendingReservations { get; set; }
        public int ConfirmedReservations { get; set; }
        public int CancelledReservations { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal RevenueThisMonth { get; set; }
    }
}
