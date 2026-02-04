using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;

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
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Product model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                UserId = HttpContext.Request.Cookies["UserId"],
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                DateCreated = DateTime.UtcNow
            };

            if (model.FormImage is { Length: > 0 })
            {
                using var memoryStream = new MemoryStream();
                await model.FormImage.CopyToAsync(memoryStream);
                product.Photo = memoryStream.ToArray();
            }

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
