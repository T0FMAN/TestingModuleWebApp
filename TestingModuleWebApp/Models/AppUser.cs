using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingModuleWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        [ForeignKey("Tutor")]
        public int? TutorId { get; set; }
        public Tutor? Tutor { get; set; }
    }
}
