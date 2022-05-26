using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupRepository.GetAll();

            return View(groups);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var VM = new CreateGroupViewModel();

            return View(VM);
        }

        [HttpPost]
        public IActionResult Create(CreateGroupViewModel VM)
        {
            if (!ModelState.IsValid)
                return View(VM);

            _groupRepository.Add(new Group { Title = VM.Title });

            return RedirectToAction("Index");
        }
    }
}
