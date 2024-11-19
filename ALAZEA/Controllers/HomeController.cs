using ALAZEA.Data;
using ALAZEA.Models;
using ALAZEA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace ALAZEA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // Helper method to get logged-in user
        private User GetLoggedInUser()
        {
            var userId = HttpContext.Session.GetString("UserID");
            if (userId != null)
            {
                return _context.User.FirstOrDefault(u => u.UserID.ToString() == userId);
            }
            return null;
        }

        // Simplified actions
        public IActionResult Index()
        {
            var model = new BaseViewModel
            {
                User = GetLoggedInUser()
            };
            return View(model);
        }

        public IActionResult About()
        {
            var model = new BaseViewModel
            {
                User = GetLoggedInUser()
            };
            return View(model);
        }

        public IActionResult Shop()
        {
            var user = GetLoggedInUser();
            var products = new List<Plant>
            {
                new Plant { Name = "Cactus Flower", ImagePath = "~/alazea_template/img/bg-img/40.png", Price = 10.99m },
                new Plant { Name = "Cactus Flower", ImagePath = "~/alazea_template/img/bg-img/40.png", Price = 10.99m },
            };

            var model = new ShopViewModel
            {
                User = user,
                Plants = products
            };

            return View(model);
        }

        public IActionResult Portfolio()
        {
            var model = new BaseViewModel
            {
                User = GetLoggedInUser()
            };
            return View(model);
        }

        public IActionResult Contact()
        {
            var model = new BaseViewModel
            {
                User = GetLoggedInUser()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
