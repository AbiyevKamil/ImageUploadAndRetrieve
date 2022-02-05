using ImageUploadAndRetrieve.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ImageUploadAndRetrieve.Data;
using ImageUploadAndRetrieve.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageUploadAndRetrieve.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            var images = await _context.Images.ToListAsync();
            return View(images);
        }

        public async Task<ActionResult> AddImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddImage(AddImageModel model)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(model.File.FileName);
                string extension = Path.GetExtension(model.File.FileName);

                string path = filename + new Guid() + extension;

                var img = new Image()
                {
                    Title = model.Title,
                    Name = path,
                };

                string filePath = Path.Combine(wwwRootPath + "/images", path);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fs);
                }

                await _context.Images.AddAsync(img);
                await _context.SaveChangesAsync();
            }
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