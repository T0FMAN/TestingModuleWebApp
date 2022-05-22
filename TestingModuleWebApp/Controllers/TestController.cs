using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IPhysicTaskRepository _physicTaskRepository;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public TestController(UserManager<AppUser> userManager, 
                              SignInManager<AppUser> signInManager, 
                              IGroupRepository groupRepository, 
                              IAppUserRepository appUserRepository,
                              IPhysicTaskRepository physicTaskRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _groupRepository = groupRepository;
            _appUserRepository = appUserRepository;
            _physicTaskRepository = physicTaskRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PhysicTask()
        {
            var user = await _appUserRepository.GetByContext(User);

            if (user.TestId == null || user.TestId == 0)
            {
                var taskDB = SetTask();

                var taskAdd = _physicTaskRepository.Add(taskDB);

                if (taskAdd.Item1)
                {
                    user.TestId = taskAdd.Item2;

                    var userAddTask = _appUserRepository.Update(user);

                    if (userAddTask)
                        return View(taskDB);
                    else
                        return RedirectToAction("Index", "Error");
                }
                else
                    return RedirectToAction("Index", "Error");
            }
            else
                return View(user.Test);
        }

        static PhysicTask SetTask()
        {
            var x0 = GetNumber(false, 0, 20);
            var u0 = GetNumber(true, 1, 20);
            var a0 = GetNumber(false, 1, 20);

            var task = new PhysicTask
            {
                x0 = x0,
                u0 = u0,
                a0 = a0,
                m = GetNumber(false, 1, 15),
                t = GetNumber(false, 3, 15),
                zE = GetNumber(false, 5, 55),
                R = GetNumber(false, 2, 15),

                TaskText = $"x = {x0}{FormatU0(u0)}t + {a0 / 2}t²",
            };

            static string FormatU0(double number)
            {
                if (number < 0)
                    return $" - {number * -1}";
                else return $" + {number}";
            }

            static double GetNumber(bool isRndEqual, int min, int max)
            {
                Random random = new();

                int number = 0;

                while (number == 0)
                    number = random.Next(min, max);

                if (isRndEqual)
                {
                    int equal = random.Next(2);

                    if (equal == 0)
                        number *= -1;
                }

                return number;
            }

            return task;
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> GetResponse(string arrValues, string arrTitles)
        {
            try
            {
                var titles = JsonConvert.DeserializeObject<List<string>>(arrTitles)!;
                var values = JsonConvert.DeserializeObject<List<string>>(arrValues)!;

                var user = await _appUserRepository.GetByContext(User);

                var taskCalc = Calculate(user.Test!, values, titles);

                if (!User.IsInRole("user"))
                    return Json(taskCalc.Item2);

                PrepareMail(user.Test!.TaskText, taskCalc, user);

                user.TestId = null;

                _appUserRepository.Update(user);

                return Json(taskCalc.Item2);
            }
            catch (Exception ex)
            {
                return Json($"Ошибка - {ex.Message}");
            }
        }

        static Tuple<List<string>, double> Calculate(PhysicTask task, List<string> values, List<string> titles)
        {
            double m = task.m; // масса тела по условию
            double t = task.t; // время по условию
            double zE = task.zE; // затраченная энергия по условию задачи
            double R = task.R; // радиус по условию задачи

            List<object> result = new();

            double x0 = task.x0; // начальная координата
            result.Add(x0);
            double u0 = task.u0; // начальная скорость 
            result.Add(u0);
            double a0 = task.a0; // (a) касательное ускорение
            result.Add(a0);
            double p0 = m * u0; // начальный импульс тела
            result.Add(p0);
            double Ek0 = m * Math.Pow(u0, 2) / 2; // начальная кинетическая энергия
            result.Add(Ek0);
            double u = u0 + a0 * t; // скорость тела через t
            result.Add(u);
            double p = m * u; // импульс тела через t
            result.Add(p);
            double R_p = p - p0; // изменение импульса тела за t
            result.Add(R_p);
            double Ft = R_p; // импульс силы
            result.Add(Ft);
            double Ek = m * Math.Pow(u, 2) / 2; // кинетическая энергия за t
            result.Add(Ek);
            double R_Ek = Ek - Ek0; // изменение кинетической энергии
            result.Add(R_Ek);
            double F = m * a0; // равнодействующая сила
            result.Add(F);
            double S = u0 * t + a0 * Math.Pow(t, 2) / 2; // перемещение за t
            result.Add(S);
            double A = R_Ek; // работа равнодействующей силы в течении t
            result.Add(A);
            double N = A / t; // мощность механическая
            result.Add(N);
            double n = A / zE * 100;  // КПД при затраченной энергии zE
            result.Add(n);
            double an = Math.Pow(u0, 2) / R; // начальное центростремительное ускорение при движении по окружности
            result.Add(an);
            double a = Math.Round(Math.Sqrt(Math.Pow(an, 2) + Math.Pow(a0, 2)), 2); // полное ускорение в начальный момент времени
            result.Add(a);

            var listResponse = new List<string>();

            int i = 0;
            int right = 0;

            foreach (var title in titles)
            {
                var trueAns = result[i].ToString();
                var userAns = values[i];

                var isTrue = "Неправильно";

                if (trueAns == userAns)
                {
                    isTrue = "Правильно";
                    right++;
                }

                string mailString = $"\n\n{title}:\n" +
                                    $"Правильный ответ: {trueAns}\n" +
                                    $"Ответ пользователя: {userAns} ({isTrue})"; 

                listResponse.Add(mailString);

                i++;
            }

            double percent = right * 100 / values.Count;

            return new Tuple<List<string>, double>(listResponse, percent);
        }

        [Authorize]
        static void PrepareMail(string task, Tuple<List<string>, double> result, AppUser user)
        {
            var name = user.FirstName;
            var lastname = user.LastName;
            var groupTitle = user.Group?.Title;

            string messageBody = $"Задача: {task}";

            foreach (var item in result.Item1)
            {
                messageBody += item;
            }

            string messageSubject = $"Новое решение сквозной задачи по физике от " +
                                    $"{lastname} {name} {groupTitle}";

            string message = $"Правильных ответов {result.Item2}%\n\n" + messageBody;

            var displayName = $"{user.Tutor?.LastName} {user.Tutor?.FirstName} {user.Tutor?.ThirdName}";
            var fromAdress = new MailAddress("rucraccou@gmail.com", "Модуль тестирования");
            var toAdress = new MailAddress("valera.elikomov2@gmail.com", displayName);

            var mailer = new MailerMessage(fromAdress, toAdress, message, messageSubject);

            SendMail(mailer);
        }

        static void SendMail(MailerMessage mailer)
        {
            try
            {
                MailMessage message = new(mailer.FromAdress, mailer.ToAdress);

                message.Body = mailer.Body;
                message.Subject = mailer.Subject;

                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailer.FromAdress.Address, "fioletovo21"),
                };

                smtpClient.Send(message);
            }
            catch (Exception ex) { }
        }
    }
}
