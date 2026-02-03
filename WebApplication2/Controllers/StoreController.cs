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
            return View(new ProductCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                UserId = string.Empty,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                DateCreated = DateTime.UtcNow
            };

            if (model.Image is { Length: > 0 })
            {
                using var memoryStream = new MemoryStream();
                await model.Image.CopyToAsync(memoryStream);
                product.Photo = memoryStream.ToArray();
            }

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
