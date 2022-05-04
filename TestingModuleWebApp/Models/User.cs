using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestingModuleWebApp.Data.Enum;

namespace TestingModuleWebApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //[ForeignKey("Group")]
        //public int GroupId { get; set; }
        //public Group Group{ get; set; }
        public Groups GroupId { get; set; }
        public bool IsAdmin { get; set; }
    }

    //public class Group
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //}
}
