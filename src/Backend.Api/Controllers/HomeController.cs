
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

public sealed class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        ViewBag.Pesan = "Welcome to Home Screen";
        return View();
    }
}