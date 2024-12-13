using ALAZEA.Data;
using ALAZEA.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;


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

        public JsonResult GetAllPlants()
        {
            try
            {
                var plants = _context.Plant.ToList();
                var result = plants.Select(p => new
                {
                    PlantID = p.PlantID,
                    Name = p.Name,
                    Category = p.Category.ToString(),
                    Description = p.Description,
                    Price = p.Price,
                    Availability = p.Availability ? "In Stock" : "Out of Stock",
                    ImagePath = string.IsNullOrEmpty(p.ImagePath) ? null : p.ImagePath.Replace("\\", "/") // Handle null cases
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPlant(Plant plant, IFormFile plantImage)
        {
            if (ModelState.IsValid)
            {

                if (plantImage != null)
                {

                    if (plantImage.Length > 0)
                    {


                        var file = plantImage;
                        var fileName = Guid.NewGuid().ToString() + file.FileName;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        plant.ImagePath = Path.Combine("/Uploads", fileName).Replace("\\", "/");

                    }
                }

                _context.Add(plant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Plants));
            }

            return View(plant);
        }

        public JsonResult GetPlantById(Guid id)
        {
            try
            {
                var plant = _context.Plant.FirstOrDefault(p => p.PlantID == id);

                if (plant == null)
                {
                    return Json(new { error = "Plant not found" });
                }

                var result = new
                {
                    PlantID = plant.PlantID,
                    Name = plant.Name,
                    Category = plant.Category.ToString(),
                    Description = plant.Description,
                    Price = plant.Price,
                    Availability = plant.Availability,
                    Tags = plant.Tags,
                    ImagePath = plant.ImagePath
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdatePlant(Plant model, IFormFile plantImage)
        {
            try
            {
                var plant = _context.Plant.FirstOrDefault(p => p.PlantID == model.PlantID);

                if (plant == null)
                {
                    return Json(new { error = "Plant not found" });
                }

                plant.Name = model.Name;
                plant.Category = model.Category;
                plant.Description = model.Description;
                plant.Price = model.Price;
                plant.Availability = model.Availability;
                plant.Tags = model.Tags;

                if (plantImage != null && plantImage.Length > 0)
                {
                    // If the plant already has an image, delete the old one
                    if (!string.IsNullOrEmpty(plant.ImagePath))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", plant.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            try
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                            catch (Exception ex)
                            {
                                // Log the exception or handle it appropriately
                                return Json(new { error = "Failed to delete old image: " + ex.Message });
                            }
                        }
                    }

                    // Save the new image
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(plantImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        plantImage.CopyTo(fileStream);
                    }
                    plant.ImagePath = Path.Combine("/Uploads", fileName).Replace("\\", "/");
                }
                else
                {
                    plant.ImagePath = plant.ImagePath;
                }

                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeletePlant(Guid id)
        {
            try
            {
                var plant = _context.Plant.FirstOrDefault(p => p.PlantID == id);
                if (plant == null)
                {
                    return Json(new { success = false, error = "Plant not found." });
                }


                if (!string.IsNullOrEmpty(plant.ImagePath))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", plant.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        try
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false, error = "Failed to delete the image file: " + ex.Message });
                        }
                    }
                }


                _context.Plant.Remove(plant);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
