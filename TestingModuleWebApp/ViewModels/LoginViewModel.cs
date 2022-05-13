using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email обязателен")]
        public string EmailAddress { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
