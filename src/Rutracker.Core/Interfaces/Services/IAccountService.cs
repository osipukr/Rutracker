﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Core.Entities.Identity;

namespace Rutracker.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<User> CreateUserAsync(string userName, string email, string password);
        Task<User> CheckUserAsync(string userName, string password);
        Task<IReadOnlyList<string>> GetUserRolesAsync(User user);
        Task UpdateUserAsync(User user);
        Task LogOutUserAsync();
    }
}