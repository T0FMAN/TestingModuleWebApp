using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface IAspNetUserRepository
    {
        Task<IEnumerable<AppUser>> GetAll();
        Task<AppUser> GetByIdAsync(string id);
        Task<IEnumerable<AppUser>> GetByGroup(string group);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
