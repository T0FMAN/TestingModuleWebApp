using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Repository
{
    public class TutorRepository : ITutorRepository
    {
        private readonly ApplicationDbContext _context;

        public TutorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Tutor tutor)
        {
            _context.Add(tutor);
            return Save();
        }

        public bool Delete(Tutor tutor)
        {
            _context.Remove(tutor);
            return Save();
        }

        public bool Update(Tutor tutor)
        {
            _context.Update(tutor);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<Tutor>> GetAll()
        {
            return await _context.Tutors!.ToListAsync();
        }

        public async Task<Tutor> GetById(int id)
        {
            return await _context.Tutors!.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tutor> GetByIdNoTracking(int id)
        {
            return await _context.Tutors!.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
