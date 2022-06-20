using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Controllers
{
    [Authorize]
    public class TutorController : Controller
    {
        private readonly ILogger<TutorController> _logger;
        private readonly ITutorRepository _tutorRepository;

        public TutorController(ILogger<TutorController> logger, ITutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tutors = await _tutorRepository.GetAll();

            return View(tutors);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var tutor = await _tutorRepository.GetById(id);

            if (tutor == null)
                return View("Errors", new ErrorViewModel { RequestId = "404" });

            return View(tutor);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var createTutorViewModel = new CreateTutorViewModel();

            return View(createTutorViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(CreateTutorViewModel createTutorViewModel)
        {
            if (!ModelState.IsValid)
                return View(createTutorViewModel);

            Tutor tutor = new()
            {
                FirstName = createTutorViewModel.FirstName,
                LastName = createTutorViewModel.LastName,
                ThirdName = createTutorViewModel.ThirdName,
                Email = createTutorViewModel.Email,
            };

            try { _tutorRepository.Add(tutor); }
            catch (Exception ex) { _logger.Log(LogLevel.Warning, ex.Message); }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tutor = await _tutorRepository.GetById(id);
            if (tutor == null) 
                return View("Error");

            var editTutorViewModel = new EditTutorViewModel
            {
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                ThirdName= tutor.ThirdName,
                Email= tutor.Email,
            };

            return View(editTutorViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTutorViewModel editTutorViewModel)
        {
            if (!ModelState.IsValid) 
                return View(editTutorViewModel);

            var checkTutor = await _tutorRepository.GetByIdNoTracking(id);

            if (checkTutor != null)
            {
                var tutor = new Tutor
                {
                    Id = id,
                    FirstName = editTutorViewModel.FirstName,
                    LastName = editTutorViewModel.LastName,
                    ThirdName = editTutorViewModel.ThirdName,
                    Email = editTutorViewModel.Email,
                };

                try { _tutorRepository.Update(tutor); }
                catch (Exception ex) { _logger.Log(LogLevel.Warning, ex.Message); }

                return RedirectToAction("Index");
            }
            else 
                return View(editTutorViewModel);
        }
    }
}
