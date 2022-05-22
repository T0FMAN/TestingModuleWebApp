using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.Models
{
    public class PhysicTask
    {
        [Key]
        public int Id { get; set; }
        public string TaskText { get; set; } // текст задачи
        // начальные данные
        public double m { get; set; } // масса тела
        public double R { get; set; } // окружность
        public double zE { get; set; } // затраченная энергия
        public double t { get; set; } // время
        public double x0 { get; set; } // начальная координата
        public double u0 { get; set; } // начальная скорость
        public double a0 { get; set; } // касательное ускорение. Ускорение тела
    }

    public class PhysicTaskViewModel
    {
        [JsonProperty("x0")]
        public double x0 { get; private set; }
        [JsonProperty("u0")]
        public double u0 { get; private set; }
        [JsonProperty("a0")]
        public double a0 { get; private set; }
        [JsonProperty("p0")]
        public double p0 { get; private set; } // начальный импульс тела
        [JsonProperty("Ek0")]
        public double Ek0 { get; private set; } // начальная кинетическая энергия
        [JsonProperty("u")]
        public double u { get; private set; } // скорость тела через t = 3C
        [JsonProperty("p")]
        public double p { get; private set; } // импульс тела через 3с.
        [JsonProperty("Rp")]
        public double Rp { get; private set; } // изменение импульса тела за 3с.
        [JsonProperty("Ft")]
        public double Ft { get; private set; } // импульс силы
        [JsonProperty("Ek")]
        public double Ek { get; private set; } // кинетическая энергия за 3с.
        [JsonProperty("REk")]
        public double REk { get; private set; } // изменение кинетической энергии
        [JsonProperty("F")]
        public double F { get; private set; } // равнодействующая сила
        [JsonProperty("S")]
        public double S { get; private set; } // перемещение за 3с.
        [JsonProperty("A")]
        public double A { get; private set; } // работа равнодействующей силы в течении 3с.
        [JsonProperty("N")]
        public double N { get; private set; } // мощность механическая
        [JsonProperty("n")]
        public double n { get; private set; }  // КПД при затраченной энергии 48Дж
        [JsonProperty("an")]
        public double an { get; private set; } // начальное центростремительное ускорение при движении по окружности
        [JsonProperty("a")]
        public double a { get; private set; } // полное ускорение в начальный момент времени
    }
}
