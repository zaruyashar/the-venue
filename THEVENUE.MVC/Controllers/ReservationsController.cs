using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using THEVENUE.MVC.Models;
using THEVENUE.MVC.Services;

namespace THEVENUE.MVC.Controllers;

[Route("admin/reservations")]
public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IVenueService _venueService;
    private readonly IEventService _eventService;

    public ReservationsController(
        IReservationService reservationService,
        IVenueService venueService,
        IEventService eventService)
    {
        _reservationService = reservationService;
        _venueService = venueService;
        _eventService = eventService;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var reservations = await _reservationService.GetAllAsync();
        return View(reservations);
    }

    [Route("create")]
    public async Task<IActionResult> Create()
    {
        await PopulateDropdowns();
        return View(new Reservation());
    }

    [Route("create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdowns();
            return View(reservation);
        }
        await _reservationService.CreateAsync(reservation);
        TempData["Success"] = "Reservation created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Route("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var reservation = await _reservationService.GetByIdAsync(id);
        if (reservation is null) return NotFound();
        await PopulateDropdowns(reservation.VenueId, reservation.EventId);
        return View(reservation);
    }

    [Route("edit/{id:int}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Reservation reservation)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdowns(reservation.VenueId, reservation.EventId);
            return View(reservation);
        }
        reservation.ReservationId = id;
        await _reservationService.UpdateAsync(reservation);
        TempData["Success"] = "Reservation updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Route("update-status/{id:int}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        await _reservationService.UpdateStatusAsync(id, status);
        TempData["Success"] = "Status updated.";
        return RedirectToAction(nameof(Index));
    }

    [Route("delete/{id:int}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _reservationService.DeleteAsync(id);
        TempData["Success"] = "Reservation deleted.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateDropdowns(int? selectedVenueId = null, int? selectedEventId = null)
    {
        var venueLookups = await _venueService.GetLookupsAsync();
        var eventLookups = await _eventService.GetLookupsAsync();

        ViewBag.Venues = new SelectList(venueLookups, "VenueId", "Name", selectedVenueId);
        ViewBag.Events = new SelectList(
            eventLookups.Select(e => new {
                e.EventId,
                Display = $"{e.Title} — {e.VenueName} ({e.StartDate:dd MMM yyyy})"
            }),
            "EventId", "Display", selectedEventId);
    }
}