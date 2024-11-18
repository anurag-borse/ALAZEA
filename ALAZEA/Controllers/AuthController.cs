using ALAZEA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ALAZEA.Data;

namespace ALAZEA.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;

        // Inject dependencies
        public AuthController(ApplicationDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Display Login Form
        public IActionResult Login()
        {
            return View();
        }

        // Display Registration Form
        // Register GET method
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Renders the same view where both forms (login & register) are located
        }

        // Handle Login Form submission
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                TempData["Error"] = "Invalid login credentials.";
                return RedirectToAction("Login");
            }

            if (user.Password == password)
            {
                HttpContext.Session.SetString("UserID", user.UserID.ToString());
                TempData["Success"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Invalid login credentials.";
            return RedirectToAction("Login");
        }

        // Handle Registration Form submission
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.User.Any(u => u.Email == user.Email))
                {
                    TempData["Error"] = "Email already in use.";
                    return RedirectToAction("Login");
                }

                user.UserID = Guid.NewGuid();
                _context.User.Add(user);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("UserID", user.UserID.ToString());

                TempData["Success"] = "Registration successful!";
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "There was an error with the registration.";
            return RedirectToAction("Login");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserID");
            TempData["Success"] = "You have logged out successfully.";
            return RedirectToAction("Index", "Home");
        }

    }
}
