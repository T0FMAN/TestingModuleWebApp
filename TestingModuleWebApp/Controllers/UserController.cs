using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Controllers
{
    [Authorize(Roles = "admin, tutor")]
    public class UserController : Controller
    {
        private readonly IAppUserRepository _appUserRepository;

        public UserController(IAppUserRepository appUser)
        {
            _appUserRepository = appUser;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _appUserRepository.GetAll();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _appUserRepository.GetById(id);

            return View(user);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var createUserViewModel = new CreateUserViewModel();

            return View(createUserViewModel);
        }
        
        [HttpPost]
        public IActionResult Create(AppUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _appUserRepository.Add(user);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var user = await _aspNetUserRepo.GetByIdAsync(id);

            //if (user == null) return View("Error");

            //var userVM = new EditUserViewModel
            //{
            //    Username = user.Username,
            //    Password = user.Password,
            //    Email = user.Email,
            //    GroupId = user.GroupId,
            //    IsAdmin = user.IsAdmin,
            //};
            //return View(userVM);
            return View();
        }
        
        //[HttpPost]
        //public IActionResult Edit(int id)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    ModelState.AddModelError(string.Empty, "Неуспешная попытка редактирования пользователя");
        //    //    return View("Edit", editUserViewModel);
        //    //}
        //    //var user = new AspNetUser
        //    //{
        //    //    UserName = editUserViewModel.UserName,
        //    //    Email = editUserViewModel.Email,
        //    //    GroupId = editUserViewModel.GroupId,
        //    //};

        //    //_aspNetUserRepo.Update(user);

        //    return View("Index");
        //}
    }
}
