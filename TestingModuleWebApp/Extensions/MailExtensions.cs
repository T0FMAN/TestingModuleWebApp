using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Extensions
{
    public static class MailExtensions
    {
        public static void SendMail(this PhysicTask task, string groupTitle)
        {
            var messageText = PrepareBodyMessage(task);
        }

        static string PrepareBodyMessage(PhysicTask task)
        {
            var title = $"Задача: {task.TaskText}\n" +
                        $"Правильно: {task.Percent}%\n";

            var answers = 
                $"Начальная координата (X₀)".TemplateAnswer(task.x0, task._x0, task.is_x0) +
                $"Начальная скорость (U₀)".TemplateAnswer(task.u0, task._u0, task.is_u0) +
                $"Касательное ускорение. Ускорение тела (a)".TemplateAnswer(task.a0, task._a0, task.is_a0) +
                $"Начальный импульс тела (p₀)".TemplateAnswer(task.p0, task._p0, task.is_p0) +
                $"Начальная кинетическая энергия (Eₖ₀)".TemplateAnswer(task.Ek0, task._Ek0, task.is_Ek0) +
                $"Скорость тела через {task.t}c (U)".TemplateAnswer(task.u, task._u, task.is_u) +
                $"Кинетическая энергия {task.t}с (Eₖ)".TemplateAnswer(task.Ek, task._Ek, task.is_Ek) +
                $"Равнодействующая сила (F)".TemplateAnswer(task.F, task._F, task.is_F) +
                $"Перемещение за {task.t}c (S)".TemplateAnswer(task.S, task._S, task.is_S) +
                $"Работа равнодействующей силы в течении {task.t}c (A)".TemplateAnswer(task.A, task._A, task.is_A) +
                $"Мощность механическая (N)".TemplateAnswer(task.N, task._N, task.is_N);

            return title + answers;
        }

        static string TemplateAnswer(this string text, double rightAns, string userAns, bool isRight)
        {
            var template =
                $"<b>{text}:</b>" +
                $"\n\nПравильный ответ: {rightAns}\n" +
                $"Ответ пользователя: {userAns} ({Bool(isRight)})";

            return template;

            static string Bool(bool value)
            {
                if (value) return "Верно";
                else return "Неверно";
            }
        }
    }
}
