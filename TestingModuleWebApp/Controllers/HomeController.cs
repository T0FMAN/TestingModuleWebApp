using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Data.Enum;
using TestingModuleWebApp.Models;
using static TestingModuleWebApp.Data.Dictionaries;

namespace TestingModuleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Solo()
        {
            return View();
        }
        
        [HttpPost]
        public async void PrepareMail(int score, string tasksArr)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    AppUser aspNetUser = await _userManager.FindByNameAsync(User.Identity.Name);

                    var tasks = JsonConvert.DeserializeObject<List<BodyTask>>(tasksArr)!;

                    var name = aspNetUser.FirstName;
                    var lastname = aspNetUser.LastName;
                    var groupId = aspNetUser.GroupId;

                    string messageBody = $"Правильных ответов {score} из {tasks.Count}\n";
                    string messageSubject = $"Новое решение сквозной задачи по физике от " +
                                            $"{lastname} {name} {GetGroup(groupId)}";

                    int i = 1;
                    foreach (var task in tasks)
                    {
                        string taskTemplate = $"Задача #{i}: {task.Task}\n" +
                                              $"Правильный ответ: {task.RightAnswer}\n" +
                                              $"Ответ пользователя: {task.UserAnswer} ({task.IsRight})\n";

                        messageBody += taskTemplate;
                        i++;
                    }

                    var fromAdress = new MailAddress("rucraccou@gmail.com", "Модуль тестирования");
                    var toAdress = new MailAddress("rea_btu@mail.ru", $"Басалгина Т.Ю");

                    var mailer = new MailerMessage(fromAdress, toAdress, messageBody, messageSubject);

                    SendMail(mailer);
                    WriteSheets();
                }
            }
            catch (Exception ex) { }
        }

        void SendMail(MailerMessage mailer)
        {
            try
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
            }
            catch (Exception ex) { _logger.Log(LogLevel.Information, ex.Message) ; }
        }

        void WriteSheets()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
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