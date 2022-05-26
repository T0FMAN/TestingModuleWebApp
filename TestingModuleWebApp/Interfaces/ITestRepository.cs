using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<Test>> GetAll();
        Task<Test> GetById(int id);
        Task<Test> GetByTitle(string name);
    }
}
