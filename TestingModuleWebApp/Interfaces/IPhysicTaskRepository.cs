using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Interfaces
{
    public interface IPhysicTaskRepository
    {
        Task<IEnumerable<PhysicTask>> GetAll();
        Task<IEnumerable<PhysicTask>> GetPassed();
        Task<IEnumerable<PhysicTask>> TestsGetByGroup(string group);
        Task<IEnumerable<GetByGroupPhysicTaskVM>> GetByGroup(string group);
        bool Add(PhysicTask physicTask);
        bool Update(PhysicTask physicTask);
        bool Delete(PhysicTask physicTask);
        bool Save();
    }
}
