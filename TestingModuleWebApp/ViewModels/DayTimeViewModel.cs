using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.ViewModels
{
    public enum DayTime
    {
        [Display(Name = "Утро")]
        Morning,
        [Display(Name = "День")]
        Afternoon,
        [Display(Name = "Вечер")]
        Evening,
        [Display(Name = "Ночь")]
        Night
    }
    public class DayTimeViewModel
    {
        public DayTime Period { get; set; }
    }
}
