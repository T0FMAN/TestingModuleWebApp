using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAll();
        bool Add(Group group);
        bool Update(Group group);
        bool Delete(Group group);
        bool Save();
    }
}
