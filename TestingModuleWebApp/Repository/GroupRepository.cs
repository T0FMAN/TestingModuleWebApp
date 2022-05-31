using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public  GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Group group)
        {
            _context.Add(group);
            return Save();
        }

        public bool Delete(Group group)
        {
            _context.Remove(group);
            return Save();
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await _context.Groups!.AsNoTracking().ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Group group)
        {
            _context.Update(group);
            return Save();
        }
    }
}
