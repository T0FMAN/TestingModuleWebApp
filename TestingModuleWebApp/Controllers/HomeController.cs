using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAppUserRepository _appUserRepository;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IGroupRepository groupRepository, IAppUserRepository appUserRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _groupRepository = groupRepository;
            _appUserRepository = appUserRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var groups = await _groupRepository.GetAll();

            var titlesGroups = groups.Select(x => x.Title).ToList();

            ViewBag.Groups = new SelectList(titlesGroups);

            return View();
        }

        [HttpGet]
        public IActionResult Solo()
        {
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public async void PrepareMail(int score, string tasksArr)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole("user"))
            {
                var appUser = await _appUserRepository.GetByContext(User);

                var tasks = JsonConvert.DeserializeObject<List<BodyTask>>(tasksArr)!;

                var name = appUser.FirstName;
                var lastname = appUser.LastName;
                var groupTitle = appUser.Group?.Title;

                string messageBody = $"Правильных ответов {score} из {tasks.Count}\n";
                string messageSubject = $"Новое решение сквозной задачи по физике от " +
                                        $"{lastname} {name} {groupTitle}";

                int i = 1;
                foreach (var task in tasks)
                {
                    string taskTemplate = $"Задача #{i}: {task.Task}\n" +
                                          $"Правильный ответ: {task.RightAnswer}\n" +
                                          $"Ответ пользователя: {task.UserAnswer} ({task.IsRight})\n";

                    messageBody += taskTemplate;
                    i++;
                }

                var displayName = $"{appUser.Tutor?.LastName} {appUser.Tutor?.FirstName} {appUser.Tutor?.ThirdName}";
                var fromAdress = new MailAddress("rucraccou@gmail.com", "Модуль тестирования");
                var toAdress = new MailAddress("valera.elikomov2@gmail.com", displayName);

                var mailer = new MailerMessage(fromAdress, toAdress, messageBody, messageSubject);

                SendMail(mailer);
                WriteSheets();
            }
        }

        void SendMail(MailerMessage mailer)
        {
                MailMessage message = new(mailer.FromAdress, mailer.ToAdress);

                message.Body = mailer.Body;
                message.Subject = mailer.Subject;

                SmtpClient smtpClient = new();

                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(mailer.FromAdress.Address, "fioletovo21");

                smtpClient.Send(message);
            
           // catch (Exception ex) { _logger.Log(LogLevel.Warning, ex.Message) ; }
        }

        void WriteSheets()
        {

        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}