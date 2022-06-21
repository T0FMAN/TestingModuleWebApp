using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using TestingModuleWebApp.Extensions;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;
using static TestingModuleWebApp.Extensions.PhyExtensions;

namespace TestingModuleWebApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ITestRepository _testRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IPhysicTaskRepository _physicTaskRepository;

        public TestController(IGroupRepository groupRepository,
                              IAppUserRepository appUserRepository,
                              IPhysicTaskRepository physicTaskRepository,
                              ITestRepository testRepository)
        {
            _groupRepository = groupRepository;
            _appUserRepository = appUserRepository;
            _physicTaskRepository = physicTaskRepository;
            _testRepository = testRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetGroupsTitle()
        {
            var groups = await _groupRepository.GetAllWithOrOutArchive(false);

            var groupTitles = new List<string>();

            foreach (var group in groups)
                groupTitles.Add(group.Title);

            return Json(groupTitles);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tests = await _testRepository.GetAll();

            return View(tests);
        }

        [HttpGet]
        public async Task<IActionResult> Browse(int id)
        {
            var test = await _testRepository.GetById(id);

            if (test == null)
                return View("Not Found");

            if (test.IsPreSetup)
                return RedirectToAction(test.Action);

            return View("Template", test);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Results(string title, string group)
        {
            var groups = await _groupRepository.GetAllWithOrOutArchive(false);

            if (title is null)
                return View("Not Found");

            var test = await _testRepository.GetByTitle(title);

            if (test == null)
                return View("Error", new ErrorViewModel { Error = $"Теста с названием '{title}' не существует" });

            if (test.IsPreSetup)
            {
                if (group != null)
                {
                    ViewData["Group"] = group;

                    //var viewModel = await _physicTaskRepository.GetByGroup(group);

                    var passedTests = await _physicTaskRepository.TestsGetByGroup(group);

                    var tuple = new Tuple<IEnumerable<PhysicTask>, IEnumerable<Group>>(passedTests, groups);

                    return View("ResultsPhysicTask", tuple);

                    //return View("FilterByGroup", viewModel);
                }

                switch (test.Id)
                {
                    case 1:
                        var passedTests = await _physicTaskRepository.GetAll();

                        var tuple = new Tuple<IEnumerable<PhysicTask>, IEnumerable<Group>>(passedTests, groups);

                        return View("ResultsPhysicTask", tuple);

                    default:
                        return View("NotFound");
                }
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeleteResult(int id)
        {
            var delete = _physicTaskRepository.Delete(new PhysicTask { Id = id });

            return RedirectToAction("Results", routeValues: new { @title = "Сквозная задача по физике" });
        }

        [HttpGet]
        public IActionResult PhysicTask()
        {
            var model = SetTask();

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetResponse(string phyTask, string selectedGroup, string selectedName, string selectedFam)
        {
            try
            {
                var task = JsonConvert.DeserializeObject<PhysicTask>(phyTask)!;

                var calcTask = task.Calculate();

                var groupId = await _groupRepository.GetIdByTitle(selectedGroup);

                if (groupId == 0)
                    return Json("Ошибка поиска группы");

                calcTask.GroupId = groupId;
                calcTask.Name = selectedName;
                calcTask.LastName = selectedFam;
                calcTask.DateTime = DateTime.Now;

                calcTask.SendMail(selectedGroup);

                var add = _physicTaskRepository.Add(calcTask);

                return Json(calcTask.Percent);
            }
            catch (Exception ex)
            {
                return Json($"Ошибка - {ex.Message}");
            }
        }
    }
}
