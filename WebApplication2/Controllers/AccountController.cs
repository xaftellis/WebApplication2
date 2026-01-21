using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Data.Entities;
using WebApplication2.Migrations;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AccountController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

       [HttpPost]
        public async Task<IActionResult> Create(AddUserViewModel viewModel)
        {
            // Check if email or username already exists
            
            var existingEmail = dbContext.Users.FirstOrDefault(u => u.Email == viewModel.Email);
            var existingUser = dbContext.Users.FirstOrDefault(u => u.Username == viewModel.Username);
            
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "An account with this email already exists.");
            }

            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username already exists");
            }

            // Only return if there were any errors
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new User
            {
                Username = viewModel.Username,
                Email = viewModel.Email,
                Password = viewModel.Password,
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> List()
        //{
        //    var users = await dbContext.Users.ToListAsync();
        //}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AddUserViewModel viewModel)
        {
            var users = dbContext.Users.ToList();
            users.FirstOrDefault();

            var user = dbContext.Users.FirstOrDefault(i =>i.Email.ToLower() == viewModel.Email.ToLower());

            if (user == null || user.Password != viewModel.Password)
            {
                ModelState.AddModelError("", "");
                ModelState.AddModelError("Password", "Invalid email or password");

                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
