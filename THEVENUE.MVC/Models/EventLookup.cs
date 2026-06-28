namespace THEVENUE.MVC.Models;

public class EventLookup
{
    public int EventId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string VenueName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
}