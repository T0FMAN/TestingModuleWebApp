using Microsoft.AspNetCore.Mvc;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository UserRepo)
        {
            _userRepo = UserRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepo.GetAll();
            return View(users);
        }
        [HttpGet]
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
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _userRepo.Add(user);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user == null) return View("Error");

            var userVM = new EditUserViewModel
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                GroupId = user.GroupId,
                IsAdmin = user.IsAdmin,
            };
            return View(userVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Неуспешная попытка редактирования пользователя");
                return View("Edit", model);
            }
            var user = new User
            {
                Id = id,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                GroupId = model.GroupId,
                IsAdmin =model.IsAdmin,
            };

            _userRepo.Update(user);

            return RedirectToAction("Index");
        }
    }
}
