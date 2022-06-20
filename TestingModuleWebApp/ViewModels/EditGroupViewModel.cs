using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.ViewModels
{
    public class EditGroupViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите корректное наименование")]
        public string Title { get; set; }
    }
}
