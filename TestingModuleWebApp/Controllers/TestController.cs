using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public async Task<IActionResult> Results(string title, string group)
        {
            var groups = await _groupRepository.GetAll();

            //ViewBag.Groups = new SelectList(groups);

            if (title is null)
            {
                return View("Not Found");
            }

            var test = await _testRepository.GetByTitle(title);

            if (test.IsPreSetup)
            {
                if (group != null)
                {
                    var viewModel = await _physicTaskRepository.GetByGroup(group);

                    var tuple = new Tuple<IEnumerable<GetByGroupPhysicTaskViewModel>, string>(viewModel, group);

                    return View("FilterByGroup", tuple);
                }

                switch (test.Id)
                {
                    case 1:
                        var passedTests = await _physicTaskRepository.GetPassed();

                        var tuple = new Tuple<IEnumerable<PhysicTask>, IEnumerable<Group>>(passedTests.Reverse(), groups);

                        return View("ResultsPhysicTask", tuple);

                    default:
                        return View("NotFound");
                }
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PhysicTask()
        {
            var user = await _appUserRepository.GetByContext(User);

            if (user.PhysicTaskId == null)
            {
                var taskDB = SetTask();

                var taskAdd = _physicTaskRepository.Add(taskDB);

                if (taskAdd.Item1)
                {
                    user.PhysicTaskId = taskAdd.Item2;

                    var userAddTask = _appUserRepository.Update(user);

                    if (userAddTask)
                        return View("PhysicTask", taskDB);
                    else
                        return RedirectToAction("Index", "Error");
                }
                else
                    return RedirectToAction("Index", "Error");
            }
            else
                return View("PhysicTask", user.PhysicTask);
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
                t = 2,
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
                var values = JsonConvert.DeserializeObject<List<string>>(arrValues)!;
                var titles = JsonConvert.DeserializeObject<List<string>>(arrTitles)!;
                
                var user = await _appUserRepository.GetByContext(User);

                var task = user.PhysicTask!;

                var newTask = new PhysicTask
                {
                    Id = task.Id,
                    TaskText = task.TaskText,
                    m = task.m,
                    R = task.R,
                    zE = task.zE,
                    t = task.t,
                    x0 = task.x0,
                    u0 = task.u0,
                    a0 = task.a0,
                };

                var taskCalc = Calculate(newTask, values, titles, user.Id);

                if (!User.IsInRole("user"))
                    return Json(taskCalc.Item2);

                PrepareMail(user.PhysicTask!.TaskText, taskCalc, user);

                user.PhysicTaskId = null;
                _appUserRepository.Update(user);

                return Json(taskCalc.Item2);
            }
            catch (Exception ex)
            {
                return Json($"Ошибка - {ex.Message}");
            }
        }

        Tuple<List<string>, double> Calculate(PhysicTask task, List<string> values, List<string> titles, string userId)
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
            
            double R_p = p - p0; // изменение импульса тела за t
          
            double Ft = R_p; // импульс силы
            
            double Ek = m * Math.Pow(u, 2) / 2; // кинетическая энергия за t
            result.Add(Ek);

            double R_Ek = Ek - Ek0; // изменение кинетической энергии
            
            double F = m * a0; // равнодействующая сила
            result.Add(F);

            double S = u0 * t + a0 * Math.Pow(t, 2) / 2; // перемещение за t
            result.Add(S);

            double A = R_Ek; // работа равнодействующей силы в течении t
            result.Add(A);

            double N = A / t; // мощность механическая
            result.Add(N);

            double n = Math.Round(A / zE * 100, 2);  // КПД при затраченной энергии zE
            
            double an = Math.Round(Math.Pow(u0, 2) / R, 2); // начальное центростремительное ускорение при движении по окружности
            
            double a = Math.Round(Math.Sqrt(Math.Pow(an, 2) + Math.Pow(a0, 2)), 2); // полное ускорение в начальный момент времени
            
            return ResearchBodyMessage(result, titles, values, task, userId);
        }

        Tuple<List<string>, double> ResearchBodyMessage(List<object> result, List<string> titles, List<string> values, PhysicTask task, string userId)
        {
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

            UpdateTask(task, values, result, userId, percent);

            return new Tuple<List<string>, double>(listResponse, percent);
        }

        void UpdateTask(PhysicTask task, List<string> values, List<object> result, string userId, double percent)
        {
            task._x0 = values[0];
            task._u0 = values[1];
            task._a0 = values[2];
            task._p0 = values[3];
            task._Ek0 = values[4];
            task._u = values[5];
            task._Ek = values[6];
            task._F = values[7];
            task._S = values[8];
            task._A = values[9];
            task._N = values[10];

            task.is_x0 = GetPass(values[0], result[0]);
            task.is_u0 = GetPass(values[1], result[1]);
            task.is_a0 = GetPass(values[2], result[2]);
            task.is_p0 = GetPass(values[3], result[3]);
            task.is_Ek0 = GetPass(values[4], result[4]);
            task.is_u = GetPass(values[5], result[5]);
            task.is_Ek = GetPass(values[6], result[6]);
            task.is_F = GetPass(values[7], result[7]);
            task.is_S = GetPass(values[8], result[8]);
            task.is_A = GetPass(values[9], result[9]);
            task.is_N = GetPass(values[10], result[10]);

            task.isPass = true;
            task.UserId = userId;
            task.Percent = percent;

            try { _physicTaskRepository.Update(task); }
            catch { }
        }

        static bool GetPass(string value, object result)
        {
            if (value == result.ToString())
                return true;
            else return false;
        }

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
            var toAdress = new MailAddress(user.Tutor!.Email, displayName);

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
