using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using THEVENUE.MVC.Models;
using THEVENUE.MVC.Services;

namespace THEVENUE.MVC.Controllers;

[Route("admin/events")]
public class EventsController : Controller
{
    private readonly IEventService _eventService;
    private readonly IVenueService _venueService;

    public EventsController(IEventService eventService, IVenueService venueService)
    {
        _eventService = eventService;
        _venueService = venueService;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var events = await _eventService.GetAllAsync();
        return View(events);
    }

    [Route("create")]
    public async Task<IActionResult> Create()
    {
        await PopulateVenueDropdown();
        return View(new Event());
    }

    [Route("create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Event ev)
    {
        if (!ModelState.IsValid)
        {
            await PopulateVenueDropdown();
            return View(ev);
        }
        await _eventService.CreateAsync(ev);
        TempData["Success"] = "Event created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Route("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var ev = await _eventService.GetByIdAsync(id);
        if (ev is null) return NotFound();
        await PopulateVenueDropdown(ev.VenueId);
        return View(ev);
    }

    [Route("edit/{id:int}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Event ev)
    {
        if (!ModelState.IsValid)
        {
            await PopulateVenueDropdown(ev.VenueId);
            return View(ev);
        }
        ev.EventId = id;
        await _eventService.UpdateAsync(ev);
        TempData["Success"] = "Event updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Route("delete/{id:int}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _eventService.DeleteAsync(id);
        TempData["Success"] = "Event deleted.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateVenueDropdown(int? selectedId = null)
    {
        var lookups = await _venueService.GetLookupsAsync();
        ViewBag.Venues = new SelectList(lookups, "VenueId", "Name", selectedId);
    }
}