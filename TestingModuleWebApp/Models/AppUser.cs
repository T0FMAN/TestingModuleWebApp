using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestingModuleWebApp.Data.Enum;

namespace TestingModuleWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Groups? GroupId { get; set; }
        //[ForeignKey("Tutor")]
        //public int? TutorId { get; set; }
        //public Tutor? Tutor { get; set; }
        public string? Tutor { get; set; }
    }
    //public class Group
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //}
}
