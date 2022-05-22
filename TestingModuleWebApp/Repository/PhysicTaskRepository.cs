using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Repository
{
    public class PhysicTaskRepository : IPhysicTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public PhysicTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Tuple<bool, int> Add(PhysicTask physicTask)
        {
            _context.Add(physicTask);
            var save = Save();

            int id = physicTask.Id;

            return new Tuple<bool, int>(save, id);
        }

        public bool Delete(PhysicTask physicTask)
        {
            _context.Remove(physicTask);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool Update(PhysicTask physicTask)
        {
            _context.Update(physicTask);
            return Save();
        }
    }
}
