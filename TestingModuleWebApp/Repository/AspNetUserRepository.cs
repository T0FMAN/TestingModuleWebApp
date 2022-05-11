using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Repository
{
    public class AspNetUserRepository : IAspNetUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AspNetUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(AppUser user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Delete(AppUser user)
        {
            _context.Remove(user);
            return Save();
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await _context.AspNetUsers.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetByGroup(string group)
        {
            return null;
            //return await _context.Users.Include(i => i.Group).Where(x => x.Group.Title.Contains(group)).ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            //return await _context.Users.Include(i => i.Group).FirstOrDefaultAsync(i => i.Id == id);
            return await _context.AspNetUsers.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
