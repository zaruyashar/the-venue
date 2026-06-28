using Microsoft.AspNetCore.Mvc;
using THEVENUE.MVC.Services;

namespace THEVENUE.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IVenueService _venueService;
    private readonly IEventService _eventService;

    public HomeController(IVenueService venueService, IEventService eventService)
    {
        _venueService = venueService;
        _eventService = eventService;
    }

    public async Task<IActionResult> Index()
    {
        var venues = await _venueService.GetPublicVenuesAsync();
        var events = await _eventService.GetUpcomingPublicAsync();
        ViewBag.Venues = venues;
        ViewBag.Events = events;
        return View();
    }

    public async Task<IActionResult> VenueDetail(int id)
    {
        var venue = await _venueService.GetByIdAsync(id);
        if (venue is null) return NotFound();
        var events = await _eventService.GetByVenueAsync(id);
        ViewBag.Events = events;
        return View(venue);
    }
}