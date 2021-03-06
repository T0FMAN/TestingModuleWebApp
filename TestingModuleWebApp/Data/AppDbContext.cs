using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<PhysicTask> PhyTasks { get; set; }
    }
}
