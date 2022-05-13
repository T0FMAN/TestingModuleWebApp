using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public  GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllTitlesList()
        {
            return await _context.Groups!.Select(x => x.Title).ToListAsync();
        }
    }
}
