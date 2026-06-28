using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using THEVENUE.MVC.Models;
using THEVENUE.MVC.Services;

namespace THEVENUE.MVC.Controllers;

[Route("admin/venues")]
public class VenuesController : Controller
{
    private readonly IVenueService _venueService;

    public VenuesController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var venues = await _venueService.GetAllAsync();
        return View(venues);
    }

    [Route("create")]
    public IActionResult Create() => View(new Venue());

    [Route("create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Venue venue)
    {
        if (!ModelState.IsValid) return View(venue);
        await _venueService.CreateAsync(venue);
        TempData["Success"] = "Venue created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Route("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var venue = await _venueService.GetByIdAsync(id);
        if (venue is null) return NotFound();
        return View(venue);
    }

    [Route("edit/{id:int}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Venue venue)
    {
        if (!ModelState.IsValid) return View(venue);
        venue.VenueId = id;
        await _venueService.UpdateAsync(venue);
        TempData["Success"] = "Venue updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Route("delete/{id:int}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _venueService.DeleteAsync(id);
        TempData["Success"] = "Venue deleted.";
        return RedirectToAction(nameof(Index));
    }
}