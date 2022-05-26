using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.ViewModels
{
    public class CreateUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Укажите группу")]
        public string Group { get; set; }
    }
}
