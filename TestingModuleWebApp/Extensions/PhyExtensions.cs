using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Extensions
{
    public static class PhyExtensions
    {
        static bool IsRight(this double rightAns, string userAns)
        {
            if (rightAns == Convert.ToDouble(userAns))
                return true;
            else return false;
        }

        public static PhysicTask Calculate(this PhysicTask task)
        {
            int right = 0;

            double m = task.m; // масса тела по условию
            double t = task.t; // время по условию
            double zE = task.zE; // затраченная энергия по условию задачи
            double R = task.R; // радиус по условию задачи

            double x0 = task.x0; // начальная координата
            task.is_x0 = x0.IsRight(task._x0);
            if (task.is_x0) right++;

            double u0 = task.u0; // начальная скорость 
            task.is_u0 = u0.IsRight(task._u0);
            if (task.is_u0) right++;

            double a0 = task.a0; // (a) касательное ускорение
            task.is_a0 = a0.IsRight(task._a0);
            if (task.is_a0) right++;

            double p0 = m * u0; // начальный импульс тела
            task.is_p0 = p0.IsRight(task._p0);
            task.p0 = p0;
            if (task.is_p0) right++;

            double Ek0 = m * Math.Pow(u0, 2) / 2; // начальная кинетическая энергия
            task.is_Ek0 = Ek0.IsRight(task._Ek0);
            task.Ek0 = Ek0;
            if (task.is_Ek0) right++;

            double u = u0 + a0 * t; // скорость тела через t
            task.is_u = u.IsRight(task._u);
            task.u = u;
            if (task.is_u) right++;

            double p = m * u; // импульс тела через t

            double R_p = p - p0; // изменение импульса тела за t

            double Ft = R_p; // импульс силы

            double Ek = m * Math.Pow(u, 2) / 2; // кинетическая энергия за t
            task.is_Ek = Ek.IsRight(task._Ek);
            task.Ek = Ek;
            if (task.is_Ek) right++;

            double R_Ek = Ek - Ek0; // изменение кинетической энергии

            double F = m * a0; // равнодействующая сила
            task.is_F = F.IsRight(task._F);
            task.F = F;
            if (task.is_F) right++;

            double S = u0 * t + a0 * Math.Pow(t, 2) / 2; // перемещение за t
            task.is_S = S.IsRight(task._S);
            task.S = S;
            if (task.is_S) right++;

            double A = R_Ek; // работа равнодействующей силы в течении t
            task.is_A = A.IsRight(task._A);
            task.A = A;
            if (task.is_A) right++;

            double N = A / t; // мощность механическая
            task.is_N = N.IsRight(task._N);
            task.N = N;
            if (task.is_N) right++;

            double n = Math.Round(A / zE * 100, 2);  // КПД при затраченной энергии zE

            double an = Math.Round(Math.Pow(u0, 2) / R, 2); // начальное центростремительное ускорение при движении по окружности

            double a = Math.Round(Math.Sqrt(Math.Pow(an, 2) + Math.Pow(a0, 2)), 2); // полное ускорение в начальный момент времени

            task.Percent = right * 100 / 11;

            return task;
        }
    }
}
