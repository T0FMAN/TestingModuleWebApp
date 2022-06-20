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
            var groups = await _groupRepository.GetAllWithOrOutArchive(false);

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var group = await _groupRepository.GetById(id);

            if (group == null)
                return View("Error");

            var VM = new EditGroupViewModel
            {
                Id = group.Id,
                Title = group.Title,
            };

            return View(VM);
        }

        [HttpPost]
        public IActionResult Edit(EditGroupViewModel VM)
        {
            if (!ModelState.IsValid)
                return View(VM);

            _groupRepository.Update(new Group { Id = VM.Id, Title = VM.Title });

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Archiving(int id, int act)
        {
            bool action = act == 1;

            var group = await _groupRepository.GetById(id);

            if (group == null)
                return View("Error", new ErrorViewModel { Error = $"В базе данных отсутсвует объект с данным ID '{id}'" });

            group.IsArchive = action;

            _groupRepository.Update(group);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Archive()
        {
            var groups = await _groupRepository.GetAllWithOrOutArchive(true);

            return View(groups);
        }
    }
}
