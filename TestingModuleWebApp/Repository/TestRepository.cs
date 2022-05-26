using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Test>> GetAll()
        {
            return await _context.Tests.ToListAsync(); 
        }

        public async Task<Test> GetById(int id)
        {
            return await _context.Tests.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Test> GetByTitle(string title)
        {
            return await _context.Tests.Where(i => i.Title == title).FirstOrDefaultAsync();
        }
    }
}
