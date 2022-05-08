using TestingModuleWebApp.Data.Enum;

namespace TestingModuleWebApp.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Groups GroupId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
