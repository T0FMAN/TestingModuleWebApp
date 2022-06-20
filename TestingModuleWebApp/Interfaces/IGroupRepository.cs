using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetById(int id);
        Task<int> GetIdByTitle(string title);
        Task<IEnumerable<Group>> GetAllWithOrOutArchive(bool isArchive);
        bool Add(Group group);
        bool Update(Group group);
        bool Delete(Group group);
        bool Save();
    }
}
