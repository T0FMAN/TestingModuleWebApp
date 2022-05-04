using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        //public DbSet<Group> Groups { get; set; }
    }
}
