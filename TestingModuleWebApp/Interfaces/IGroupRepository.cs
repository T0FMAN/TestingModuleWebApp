using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAll();
    }
}
