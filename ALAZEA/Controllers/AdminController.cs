using ALAZEA.Data;
using ALAZEA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALAZEA.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;

        // Inject dependencies
        public AdminController(ApplicationDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            var adminInDb = _context.Admin.FirstOrDefault(u => u.Username == admin.Username);
            if (adminInDb == null)
            {
                TempData["Error"] = "Invalid login credentials.";
                return RedirectToAction(nameof(Login));
            }

            if (adminInDb.Password == admin.Password)
            {
                HttpContext.Session.SetString("AdminID", adminInDb.AdminID.ToString());
                TempData["Success"] = "Login successful!";
                return RedirectToAction(nameof(Dashboard), nameof(Admin));
            }

            TempData["Error"] = "Invalid login credentials.";
            return RedirectToAction(nameof(Login));

        }

        [HttpGet]
        public IActionResult Dashboard()
        {

            return View();
        }


        [HttpGet]
        public IActionResult Plants()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlant(Plant plant, IFormFile plantImage)
        {
            if (ModelState.IsValid)
            {
                
                if (plantImage != null)
                {
                    var file = Request.Form.Files[0];
                    if (file.Length > 0)
                    {
                        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine("Uploads", uniqueFileName);


                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }

                        plant.ImagePath = filePath; 
                    }
                }

                _context.Add(plant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Plants)); 
            }

            return View(plant); 
        }




    }
}
