using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface ITutorRepository
    {
        bool Add(Tutor tutor);
        bool Update(Tutor tutor);
        bool Delete(Tutor tutor);
        bool Save();
        Task<IEnumerable<Tutor>> GetAll();
        Task<Tutor> GetById(int id);
        Task<Tutor> GetByIdNoTracking(int id); // если в контроллере 2 контекста БД
    }
}
