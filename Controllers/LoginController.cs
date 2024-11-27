using LogansDBTest.Data;
using LogansDBTest.Helpers;
using LogansDBTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LogansDBTest.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display login form
        public IActionResult Index()
        {
            return View(); // Show the login form
        }

        // Login action
        [HttpPost]
        public IActionResult Login(Login loginModel)
        {
             if (!HttpContext.Session.IsAvailable)
                {
                    throw new InvalidOperationException("Session is not available.");
                }
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View("Index");
            }

            // Hash the entered password
            var hashedPassword = PasswordHelper.HashPassword(loginModel.Password);

            // Check if the user exists in the database
            var user = _context.UsersAndPasswords.FirstOrDefault(u => u.Username == loginModel.Username);

            if (user != null && user.PasswordHash == hashedPassword)
            {
                // Login successful
                HttpContext.Session.SetString("LoggedInUser", loginModel.Username);
                return RedirectToAction("Index", "User"); // Redirect to the home page
            }

            // Login failed
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View("Index"); // Reload the login page with an error message
        }

        // Account creation
        [HttpPost]
        public IActionResult CreateAccount(Login loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                ViewBag.ErrorMessage = "Username and password are required to create an account.";
                return View("Index");
            }

            // Check if username already exists
            if (_context.UsersAndPasswords.Any(u => u.Username == loginModel.Username))
            {
                ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                return View("Index");
            }

            // Hash the password
            var hashedPassword = PasswordHelper.HashPassword(loginModel.Password);

            // Save the new user to the database
            var newUser = new UsersAndPasswords
            {
                Username = loginModel.Username,
                PasswordHash = hashedPassword
            };

            _context.UsersAndPasswords.Add(newUser);
            _context.SaveChanges();

            ViewBag.SuccessMessage = "Account created successfully! Please log in.";
            return View("Index");
        }
    }
}
