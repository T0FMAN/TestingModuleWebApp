using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetByGroup(string group);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
