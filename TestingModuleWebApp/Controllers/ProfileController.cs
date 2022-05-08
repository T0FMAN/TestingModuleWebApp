using Microsoft.AspNetCore.Mvc;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepo;

        public ProfileController(IUserRepository UserRepo)
        {
            _userRepo = UserRepo;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepo.GetAll();
            return View(users);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _userRepo.Add(user);

            return RedirectToAction("Index");
        }
    }
}
