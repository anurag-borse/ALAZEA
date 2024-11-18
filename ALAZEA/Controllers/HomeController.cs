using ALAZEA.Data;
using ALAZEA.Models;
using ALAZEA.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserID");
            User loggedInUser = null;

            if (userId != null)
            {
                loggedInUser = _context.User.FirstOrDefault(u => u.UserID.ToString() == userId);
            }

            var model = new BaseViewModel
            {
                User = loggedInUser
            };

            return View(model);
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Shop()
        {
            var userId = HttpContext.Session.GetString("UserID");
            User user = null;

            if (userId != null)
            {
                user = _context.User.FirstOrDefault(u => u.UserID.ToString() == userId);
            }

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
