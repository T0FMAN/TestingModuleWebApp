using Microsoft.EntityFrameworkCore;
using TestingModuleWebApp.Data;
using TestingModuleWebApp.Interfaces;
using TestingModuleWebApp.Models;
using TestingModuleWebApp.ViewModels;

namespace TestingModuleWebApp.Repository
{
    public class PhysicTaskRepository : IPhysicTaskRepository
    {
        private readonly AppDbContext _context;
        private readonly IAppUserRepository _appUserRepository;

        public PhysicTaskRepository(AppDbContext context, IAppUserRepository appUserRepository)
        {
            _context = context;
            _appUserRepository = appUserRepository;
        }

        public async Task<IEnumerable<PhysicTask>> GetAll()
        {
            return await _context.PhyTasks.AsNoTracking()
                                          .Include(n => n.Group)
                                          .OrderByDescending(n => n.DateTime)
                                          .ToListAsync();
        }

        public async Task<IEnumerable<PhysicTask>> GetPassed()
        {
            return await _context.PhyTasks.AsNoTracking()
                                             .Include(n => n.User)
                                             .Where(n => n.isPass == true)
                                             .ToListAsync();
        }

        public async Task<IEnumerable<PhysicTask>> TestsGetByGroup(string group)
        {
            return await _context.PhyTasks.AsNoTracking()
                                             .Include(n => n.Group)
                                             .Where(n => n.Group.Title == group)
                                             .OrderByDescending(n => n.DateTime)
                                             .ToListAsync();
        }

        public async Task<IEnumerable<GetByGroupPhysicTaskVM>> GetByGroup(string group)
        {
            var viewModelList = new List<GetByGroupPhysicTaskVM>();

            var tasks = await TestsGetByGroup(group);



            foreach (var task in tasks)
            {
                
            }

            //var users = await _appUserRepository.GetByGroup(group);

            //foreach (var user in users)
            //{
            //    var userTasks = tasks.Where(n => n.UserId == user.Id);

            //    var count = userTasks.Count();
            //    var best = BestAttempt(count, userTasks);

            //    var byGroupVM = new GetByGroupPhysicTaskViewModel
            //    {
            //        User = user,
            //        Count = count,
            //        BestAttempt = best,
            //    };
            //    viewModelList.Add(byGroupVM);
            //}
            return viewModelList;
        }

        static double BestAttempt(int count, IEnumerable<PhysicTask> tasks)
        {
            if (count == 0)
                return 0;
            else
                return tasks.Max(n => n.Percent);
        }

        public bool Add(PhysicTask physicTask)
        {
            _context.Add(physicTask);
            return Save();
        }

        public bool Delete(PhysicTask physicTask)
        {
            _context.Remove(physicTask);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool Update(PhysicTask physicTask)
        {
            _context.Update(physicTask);
            return Save();
        }
    }
}
