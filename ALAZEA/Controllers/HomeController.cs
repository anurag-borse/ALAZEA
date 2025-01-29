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
            var products = _context.Plant.ToList();

            var model = new ShopViewModel
            {
                User = user,
                Plants = products
            };

            return View(model);
        }



        public IActionResult ShopDetails(Guid id)
        {
            var plant = _context.Plant.FirstOrDefault(p => p.PlantID == id);
            if (plant == null)
            {
                return NotFound();
            }

            // Fetch the latest 4 plants excluding the selected one
            var relatedProducts = _context.Plant
                .Where(p => p.PlantID != id) 
                .OrderByDescending(p => p.PlantID)
                .Take(4)
                .ToList();

            var viewModel = new ShopDetailsViewModel
            {
                SelectedPlant = plant,
                RelatedProducts = relatedProducts,
                User = GetLoggedInUser()

            };

            return View(viewModel);
        }

        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult BlogDetail()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult PortfolioDetail()
        {
            return View();
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



    }
}
