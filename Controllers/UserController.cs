using Microsoft.AspNetCore.Mvc;
using LogansDBTest.Data;
using LogansDBTest.Models;
using System.Linq;

namespace LogansDBTest.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display account information
        public IActionResult Index()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(loggedInUser))
            {
                // Redirect to login page if no session exists
                return RedirectToAction("Index", "Login");
            }

            // Fetch the logged-in user's information
            var user = _context.Users.FirstOrDefault(u => u.Username == loggedInUser);

            // If no record exists, create a placeholder object for the view
            if (user == null)
            {
                user = new User
                {
                    Username = loggedInUser // Pre-populate Username
                };
            }

            return View(user); // Pass the user object to the view
        }

        // Save or update account information
        [HttpPost]
        public IActionResult Save(User user)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(loggedInUser))
            {
                // Redirect to login page if no session exists
                return RedirectToAction("Index", "Login");
            }

            // Ensure the user being saved is associated with the logged-in session
            user.Username = loggedInUser;

            var existingUser = _context.Users.FirstOrDefault(u => u.Username == loggedInUser);

            if (existingUser == null)
            {
                // Create new record
                user.Insert_TimeStamp = DateTime.Now;
                _context.Users.Add(user);
            }
            else
            {
                // Update existing record
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Insert_TimeStamp = DateTime.Now; // Update the timestamp
                _context.Users.Update(existingUser);
            }

            _context.SaveChanges();

            // Redirect back to the account page
            return RedirectToAction("Index");
        }
    }
}
