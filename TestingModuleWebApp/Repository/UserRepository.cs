using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Data.Enum;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Delete(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetByGroup(string group)
        {
            return null;
            //return await _context.Users.Include(i => i.Group).Where(x => x.Group.Title.Contains(group)).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            //return await _context.Users.Include(i => i.Group).FirstOrDefaultAsync(i => i.Id == id);
            return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
