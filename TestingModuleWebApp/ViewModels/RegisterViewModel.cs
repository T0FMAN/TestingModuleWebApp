using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email обязателен к вводу")]
        public string EmailAddress { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязателен к вводу"), MinLength(8, ErrorMessage = "Минимальная длина 8 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Подтверждение пароля")]
        [Required(ErrorMessage = "Подтверждение пароля обязателено")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
