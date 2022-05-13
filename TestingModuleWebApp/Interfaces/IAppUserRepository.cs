﻿using System.Security.Claims;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<AppUser>> GetAll();
        Task<AppUser> GetByContext(ClaimsPrincipal claims);
        Task<AppUser> GetById(string id);
        Task<IEnumerable<AppUser>> GetByGroup(int group);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
