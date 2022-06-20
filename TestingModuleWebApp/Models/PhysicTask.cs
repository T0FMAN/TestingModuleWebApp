using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingModuleWebApp.Models
{
    public class PhysicTask
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("TaskText")]
        public string TaskText { get; set; } // текст задачи
        // начальные данные
        [JsonProperty("m")]
        public double m { get; set; } // масса тела
        [JsonProperty("R")]
        public double R { get; set; } // окружность
        [JsonProperty("zE")]
        public double zE { get; set; } // затраченная энергия
        [JsonProperty("t")]
        public double t { get; set; } // время
        [JsonProperty("x0")]
        public double x0 { get; set; } // начальная координата
        [JsonProperty("u0")]
        public double u0 { get; set; } // начальная скорость
        [JsonProperty("a0")]
        public double a0 { get; set; } // касательное ускорение. Ускорение тела
        // right answers
        public double p0 { get; set; }
        public double Ek0 { get; set; }
        public double u { get; set; }
        public double Ek { get; set; }
        public double F { get; set; }
        public double S { get; set; }
        public double A { get; set; }
        public double N { get; set; }
        public bool isPass { get; set; }
        // ответы пользователя
        [JsonProperty("_x0")]
        public string _x0 { get; set; }
        [JsonProperty("_u0")]
        public string _u0 { get; set; }
        [JsonProperty("_a0")]
        public string _a0 { get; set; }
        [JsonProperty("_p0")]
        public string _p0 { get; set; }
        [JsonProperty("_Ek0")]
        public string _Ek0 { get; set; }
        [JsonProperty("_u")]
        public string _u { get; set; }
        [JsonProperty("_Ek")]
        public string _Ek { get; set; }
        [JsonProperty("_F")]
        public string _F { get; set; }
        [JsonProperty("_S")]
        public string _S { get; set; }
        [JsonProperty("_A")]
        public string _A { get; set; }
        [JsonProperty("_N")]
        public string _N { get; set; }
        // правильно или ложно
        public bool is_x0 { get; set; } = false;
        public bool is_u0 { get; set; } = false;
        public bool is_a0 { get; set; } = false;
        public bool is_p0 { get; set; } = false;
        public bool is_Ek0 { get; set; } = false;
        public bool is_u { get; set; } = false;
        public bool is_Ek { get; set; } = false;
        public bool is_F { get; set; } = false;
        public bool is_S { get; set; } = false;
        public bool is_A { get; set; } = false;
        public bool is_N { get; set; } = false;
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public double Percent { get; set; } = 0;
        public string Name { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public DateTime DateTime { get; set; }
    }
}