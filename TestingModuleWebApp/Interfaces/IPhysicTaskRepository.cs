using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Interfaces
{
    public interface IPhysicTaskRepository
    {
        Tuple<bool, int> Add(PhysicTask physicTask);
        Task<IEnumerable<PhysicTask>> GetPassed();
        Task<IEnumerable<GetByGroupPhysicTaskViewModel>> GetByGroup(string group);
        bool Update(PhysicTask physicTask);
        bool Delete(PhysicTask physicTask);
        bool Save();
    }
}
