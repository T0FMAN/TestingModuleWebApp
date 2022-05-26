using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool isPass { get; set; }
        // ответы пользователя
        public string? _x0 { get; set; }
        public string? _u0 { get; set; }
        public string? _a0 { get; set; }
        public string? _p0 { get; set; }
        public string? _Ek0 { get; set; }
        public string? _u { get; set; }
        public string? _Ek { get; set; }
        public string? _F { get; set; }
        public string? _S { get; set; }
        public string? _A { get; set; }
        public string? _N { get; set; }
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
    }
}