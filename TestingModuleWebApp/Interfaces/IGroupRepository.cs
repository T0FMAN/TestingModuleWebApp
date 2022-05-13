using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<string>> GetAllTitlesList();
    }
}
