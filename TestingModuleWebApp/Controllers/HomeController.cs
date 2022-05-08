using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Solo()
        {
            return View();
        }
        
        [HttpPost]
        public void PrepareMail(int score, string tasksArr)
        {
            var tasks = JsonConvert.DeserializeObject<List<BodyTask>>(tasksArr)!;

            string messageBody = $"Правильных ответов {score} из {tasks.Count}\n";
            string messageSubject = "Новое решение сквозной задачи по физике";
            int i = 1;

            foreach (var task in tasks)
            {
                string taskTemplate = $"Задача #{i}: {task.Question}\n" +
                                      $"Правильный ответ: {task.RightAnswer}\n" +
                                      $"Ответ пользователя: {task.UserAnswer} ({task.IsRight})\n";

                messageBody += taskTemplate;
                i++;
            }

            var fromAdress = new MailAddress("rucraccou@gmail.com", "Модуль тестирования");
            var toAdress = new MailAddress("valera.elykomov@gmail.com", "Валерий");

            var mailerMessage = new MailerMessage(fromAdress, toAdress, messageBody, messageSubject);

            SendMail(score, mailerMessage);
        }

        private static void SendMail(int score, MailerMessage mail)
        {
            MailMessage message = new(mail.FromAdress, mail.ToAdress);

            message.Body = mail.Body;
            message.Subject = mail.Subject;

            SmtpClient smtpClient = new();

            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(mail.FromAdress.Address, "fioletovo21");

            smtpClient.Send(message);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(model: JsonConvert.SerializeObject(new BodyTask()));
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