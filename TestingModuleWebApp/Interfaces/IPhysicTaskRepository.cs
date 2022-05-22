using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface IPhysicTaskRepository
    {
        Tuple<bool, int> Add(PhysicTask physicTask);
        bool Update(PhysicTask physicTask);
        bool Delete(PhysicTask physicTask);
        bool Save();
    }
}
