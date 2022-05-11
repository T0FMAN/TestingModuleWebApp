using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.Models
{
    public class Tutor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThirdName { get; set; }
        public string Email { get; set; }
    }
}
