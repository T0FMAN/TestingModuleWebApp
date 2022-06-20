using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.ViewModels
{
    public class GetByGroupPhysicTaskViewModel
    {
        public AppUser? User { get; set; }
        public int Count { get; set; } = 0;
        public double BestAttempt { get; set; } = 0;
    }

    public class GetByGroupPhysicTaskVM
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<PhysicTask> Tasks { get; set; } = new();
        public int Count { get; set; } = 0;
        public double BestAttempt { get; set; } = 0;
    }
}
