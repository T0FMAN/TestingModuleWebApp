using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.ViewModels
{
    public class CreateTutorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Введите отчество")]
        public string? ThirdName { get; set; }
        [Required(ErrorMessage = "Введите email")]
        public string Email { get; set; }
    }
}
