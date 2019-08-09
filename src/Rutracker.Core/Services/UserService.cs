using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Rutracker.Core.Entities.Identity;
using Rutracker.Core.Exceptions;
using Rutracker.Core.Extensions;
using Rutracker.Core.Interfaces.Services;

namespace Rutracker.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<IReadOnlyList<User>> GetAllUserAsync()
        {
            var users = await Task.FromResult(_userManager.Users.ToList());

            Guard.Against.Null(nameof(users), users);

            return users;
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new TorrentException("Not valid principal.", ExceptionEventType.NotValidParameters);
            }

            var user = await _userManager.GetUserAsync(principal);

            Guard.Against.Null(nameof(user), user);

            return user;
        }

        public async Task<IReadOnlyList<Role>> GetUserRolesAsync(User user)
        {
            if (user == null)
            {
                throw new TorrentException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            var roles = await _userManager.GetRolesAsync(user);

            Guard.Against.Null(nameof(roles), roles);

            return _roleManager.Roles.Join(roles, r => r.Name, n => n, (r, n) => r).ToList();
        }
    }
}