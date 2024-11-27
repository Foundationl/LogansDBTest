using Microsoft.AspNetCore.Mvc;
using LogansDBTest.Data;
namespace LogansDBTest.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser"); 
            if (string.IsNullOrEmpty(loggedInUser))
            {
                //redirect to login page if nmo session exists
                return RedirectToAction("Index","Login");
            }
            // Fetch users from the database
            var users = _context.Users.ToList();
            return View(users); // Pass the data to the view
        }
    }
}
