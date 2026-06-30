using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using THEVENUE.MVC.Models;
using THEVENUE.MVC.Services;

namespace THEVENUE.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IVenueService _venueService;
    private readonly IEventService _eventService;
    private readonly IContactService _contactService;

    public HomeController(
        IVenueService venueService,
        IEventService eventService,
        IContactService contactService)
    {
        _venueService = venueService;
        _eventService = eventService;
        _contactService = contactService;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("ContactFormPolicy")]
    public async Task<IActionResult> SubmitContact(string name, string email, string phone, string message)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
        {
            TempData["Error"] = "Name, Email, and Message are required.";
            return RedirectToAction(nameof(Index));
        }

        var contact = new Contact
        {
            Name = name,
            Email = email,
            Phone = phone,
            Subject = "Website Enquiry",
            Message = message
        };

        await _contactService.CreateAsync(contact);
        TempData["Success"] = "Thank you! Your message has been sent successfully.";
        return RedirectToAction(nameof(Index));
    }
}