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

        public async Task<Group> GetById(int id)
        {
            return await _context.Groups.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<int> GetIdByTitle(string title)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(n => n.Title == title);

            if (group == null)
                return 0;

            return group.Id;
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

        public async Task<IEnumerable<Group>> GetAllWithOrOutArchive(bool isArchive)
        {
            return await _context.Groups!.AsNoTracking()
                                         .Where(n => n.IsArchive == isArchive)
                                         .ToListAsync();
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
