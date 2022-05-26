using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.ViewModels
{
    public class GetByGroupPhysicTaskViewModel
    {
        public AppUser? User { get; set; }
        public int Count { get; set; } = 0;
        public double BestAttempt { get; set; } = 0;
    }
}
