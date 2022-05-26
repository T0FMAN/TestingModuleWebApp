using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingModuleWebApp.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Author")]
        public string? AuthorId { get; set; }
        public AppUser? Author { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsPreSetup { get; set; }
    }
}
