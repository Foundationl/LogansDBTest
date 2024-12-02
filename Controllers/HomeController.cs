using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LogansDBTest.Models;

namespace LogansDBTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
         var loggedInUser = HttpContext.Session.GetString("LoggedInUser");

            if (!string.IsNullOrEmpty(loggedInUser))
            {
                ViewBag.LoggedInUser = loggedInUser; // Pass the username to the view
            }
            else
            {
                ViewBag.LoggedInUser = null; // No user is logged in
            }

            return View();
    }

    public IActionResult Users()
    {
        return RedirectToAction("Index", "User");
    }

     public IActionResult Login()
    {
        return RedirectToAction("Index", "Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
