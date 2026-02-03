using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using System.Drawing;

namespace WebApplication2.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StoreController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Product model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    if (model.Photo != null && model.Photo.Length > 0)
        //    {
        //        using var ms = new MemoryStream();
        //    }

        //    _context.Products.Add(model);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

    }
}
