using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.Models
{
    public class PhysicsTask
    {
        public int Mass { get; set; }

        public int R_x0 { get; set; } 

        public string _znak { get; set; }

        public int R_u0 { get; set; }

        public PhysicsTask()
        {
            Random random = new();

            Mass = random.Next(1, 10);

            R_x0 = RandomX0(random);

            R_u0 = RandomU0(random);

            _znak = Znak(R_u0);
        }

        int RandomX0(Random random)
        {
            int x0 = random.Next(1, 10);

            return x0;
        }

        string Znak(int x)
        {
            if (x > 0)
                return "+";
            else return string.Empty;
        }

        int RandomU0(Random random)
        {
            int u0 = 0;
            
            while (u0 == 0)
            {
                u0 = random.Next(-10, 15);
            }
            return u0;
        }

        //[Required]
        //public double x0 { get; set; } // начальная координата
        //[Required]
        //public double u0 { get; set; } // начальная скорость 
        //[Required]
        //public double aч { get; set; } // (a) касательное ускорение
        //[Required]
        //public double a { get; set; } // ускорение тела
        //[Required]
        //public double p0 { get; set; } // начальный импульс тела

        //public double Ek0 { get; set; } // начальная кинетическая энергия

        //public int t { get; set; } // время для условия 3с.
        //public double u { get; set; } // скорость тела через t = 3C

        //public double p { get; set; } // импульс тела через 3с.

        //public double R_p { get; set; } // изменение импульса тела за 3с.

        //public double Ft { get; set; } // импульс силы

        //public double Ek { get; set; } // кинетическая энергия за 3с.

        //public double R_Ek { get; set; } // изменение кинетической энергии

        //public double F { get; set; } // равнодействующая сила

        //public double S { get; set; } // перемещение за 3с.

        //public double A { get; set; } // работа равнодействующей силы в течении 3с.

        //public double N { get; set; } // мощность механическая

        //public int zE { get; set; } // по условию задачи
        //public double n { get; set; }  // КПД при затраченной энергии 48Дж

        //public int R { get; set; } // по условию задачи
        //public double an { get; set; } // начальное центростремительное ускорение при движении по окружности

        public double _a { get; set; } // полное ускорение в начальный момент времени

        public void Calculate()
        {
            string formula = "x0 + u0 * t + at^2 / 2"; // формула

            double m = 4; // масса тела по условию

            double x0 = 4; // начальная координата

            double u0 = -2; // начальная скорость 

            double aч = 2; // (a) касательное ускорение

            double a = aч; // ускорение тела

            double p0 = m * u0; // начальный импульс тела

            double Ek0 = m * Math.Pow(u0, 2) / 2; // начальная кинетическая энергия

            int t = 3; // время для условия 3с.
            double u = u0 + a * t; // скорость тела через t = 3C

            double p = m * u; // импульс тела через 3с.

            double R_p = p - p0; // изменение импульса тела за 3с.

            double Ft = R_p; // импульс силы

            double Ek = m * Math.Pow(u, 2) / 2; // кинетическая энергия за 3с.

            double R_Ek = Ek - Ek0; // изменение кинетической энергии

            double F = m * a; // равнодействующая сила

            double S = u0 * t + a * Math.Pow(t, 2) / 2; // перемещение за 3с.

            double A = R_Ek; // работа равнодействующей силы в течении 3с.

            double N = A / t; // мощность механическая

            int zE = 48; // по условию задачи
            double n = A / zE * 100;  // КПД при затраченной энергии 48Дж

            int R = 2; // по условию задачи
            double an = Math.Pow(u0, 2) / R; // начальное центростремительное ускорение при движении по окружности

            double _a = Math.Round(Math.Sqrt(Math.Pow(an, 2) + Math.Pow(a, 2)), 2); // полное ускорение в начальный момент времени

            string znak(double val)
            {
                var new_val = val.ToString();

                if (val < 0)
                {
                    new_val = new_val.Substring(1);

                    return $"- {new_val}";
                }
                else return $"+ {val}";
            }

            string reforms = $"{x0} {znak(u0)}t + t^2";

            //Console.WriteLine($"Тело массой m={m}кг " +
            //                  $"движется по закону:\n{formula}\n" +
            //                  $"Дано уравнение: {reforms}\n" +
            //                  $"Решение:\n****************\n" +
            //                  $"Начальная координата: {x0}м\n" +
            //                  $"Начальная скорость: {u0}м/с\n" +
            //                  $"Касательное ускорение. Ускорение тела: {a}м/с^2\n" +
            //                  $"Начальный импульс тела: {p0}кг м/с\n" +
            //                  $"Начальная кинетическая энергия: {Ek0}Дж\n" +
            //                  $"Скорость тела через t=3C: {u}м/с\n" +
            //                  $"Импульс тела через t=3C: {p}кг м/с\n" +
            //                  $"Изменение импульса тела за t=3C: {R_p}кг м/с\n" +
            //                  $"Импульс силы: {Ft}кг м/с\n" +
            //                  $"Кинетическая энергия за t=3C: {Ek}Дж\n" +
            //                  $"Изменение кинетической энергии: {R_Ek}Дж\n" +
            //                  $"Равнодействующая сила: {F}Н\n" +
            //                  $"Перемещение за t=3C: {S}м\n" +
            //                  $"Работа равнодействующей силы в течении t=3C: {A}Дж\n" +
            //                  $"Мощность механическая: {N}Вт\n" +
            //                  $"КПД при затраченной энергии 48Дж: {n}%\n" +
            //                  $"Начальное центростремительное ускорение при движении по окружности: {an}м/с^2\n" +
            //                  $"Полное ускорение в начальный момент времени: {_a}м/с^2\n");
        }
    }
}
