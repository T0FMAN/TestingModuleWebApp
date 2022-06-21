using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IAppUserRepository appUserRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
        }
        /*
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index() // личный кабинет
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "AdminPanel");
            }

            var user = await _appUserRepository.GetByContext(User);

            return View(user);
        }*/

        [HttpGet]
        public IActionResult Login()
        {
            var VM = new LoginViewModel();

            return View(VM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) 
                return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Неправильные данные";
                return View(loginViewModel);
            }
            TempData["Error"] = "Пользователь не найден";
            return View(loginViewModel);
        }
        /*
        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "Данная почта уже используется";
                return View(registerViewModel);
            }

            var newUser = new AppUser
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            else
            {
                string errors = string.Empty;
                foreach (var error in newUserResponse.Errors)
                {
                    errors += error.Description;
                }
                TempData["Error"] = "Пароль должен содержать как минимум 1 цифру, 1 заглавную, 1 строчную букву и 1 символ";
                return View(registerViewModel);
            }

            return RedirectToAction("Login", "Account");
        }
        */
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        { 
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
