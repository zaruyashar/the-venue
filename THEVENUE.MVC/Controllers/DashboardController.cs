using Microsoft.AspNetCore.Mvc;
using THEVENUE.MVC.Services;

namespace THEVENUE.MVC.Controllers;

[Area("")]
[Route("admin/dashboard")]
public class DashboardController : Controller
{
    private readonly IReservationService _reservationService;

    public DashboardController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [Route("")]
    [Route("/admin")]
    public async Task<IActionResult> Index()
    {
        var stats = await _reservationService.GetDashboardStatsAsync();
        var byVenue = await _reservationService.GetRevenueByVenueAsync();
        var byMonth = await _reservationService.GetReservationsByMonthAsync();
        var byEventType = await _reservationService.GetEventTypeBreakdownAsync();

        ViewBag.Stats = stats;
        ViewBag.ByVenue = byVenue;
        ViewBag.ByMonth = byMonth;
        ViewBag.ByEventType = byEventType;

        return View();
    }
}