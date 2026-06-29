using Microsoft.AspNetCore.Mvc;
using THEVENUE.MVC.Models;
using THEVENUE.MVC.Services;

namespace THEVENUE.MVC.Controllers;

[Route("admin/contacts")]
public class ContactsController : Controller
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var messages = await _contactService.GetAllAsync();
        return View(messages);
    }

    [Route("details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var message = await _contactService.GetByIdAsync(id);
        if (message is null) return NotFound();

        if (!message.IsRead)
        {
            await _contactService.MarkAsReadAsync(id);
            message.IsRead = true;
        }

        return View(message);
    }

    [Route("delete/{id:int}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.DeleteAsync(id);
        TempData["Success"] = "Message deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    [Route("unread-count")]
    [HttpGet]
    public async Task<IActionResult> GetUnreadCount()
    {
        var count = await _contactService.GetUnreadCountAsync();
        return Json(new { Count = count });
    }
}
