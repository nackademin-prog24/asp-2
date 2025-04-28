using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("bookings")]
public class BookingsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Bookings";
        return View();
    }
}
