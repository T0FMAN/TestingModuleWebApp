using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AppUserRepository(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<PhysicTask> GetUserPhyTask(ClaimsPrincipal claims)
        {
            var user = await GetByContext(claims);

            var task = user.PhysicTask;

            return task;
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await _context.Users!.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetByGroup(int group)
        {
            return await _context.Users.Include(i => i.Group)
                                       .Include(i => i.Tutor)
                                       .Where(i => i.GroupId == group)
                                       .ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetByGroup(string group)
        {
            return await _context.Users.Include(i => i.Group)
                                       .Include(i => i.Tutor)
                                       .Where(i => i.Group.Title == group)
                                       .ToListAsync();
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _context.Users.Include(i => i.Group)
                                       .Include(i => i.Tutor)
                                       .Include(i => i.PhysicTask)
                                       .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<AppUser> GetByContext(ClaimsPrincipal claims)
        {
            var user = await _userManager.GetUserAsync(claims);

            return await GetById(user.Id);
        }
    }
}
