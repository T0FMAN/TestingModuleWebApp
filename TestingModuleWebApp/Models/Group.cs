using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
