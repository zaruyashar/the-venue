namespace THEVENUE.API.DTOs.Shared
{
    public class ReservationsByMonthDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; } = string.Empty;
        public int ReservationCount { get; set; }
        public decimal Revenue { get; set; }
    }
}
