using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class RoleService : Service, IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Role> FindAsync(string roleId)
        {
            Guard.Against.NullInvalid(roleId, Resources.RoleService_FindAsync_The_role_id_is_invalid_);

            var role = await _roleManager.FindByIdAsync(roleId);

            Guard.Against.NullNotFound(role, string.Format(Resources.RoleService_FindAsync_, roleId));

            return role;
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            Guard.Against.NullInvalid(roleName, Resources.RoleService_FindByNameAsync_The_role_name_is_invalid_);

            var role = await _roleManager.FindByNameAsync(roleName);

            Guard.Against.NullNotFound(role, string.Format(Resources.RoleService_FindByNameAsync_, roleName));

            return role;
        }

        public async Task<Role> AddAsync(Role role)
        {
            Guard.Against.NullInvalid(role, Resources.RoleService_AddAsync_The_role_is_invalid_);

            var result = await _roleManager.CreateAsync(role);

            Guard.Against.IsSucceeded(result);

            return role;
        }

        public async Task<bool> ExistAsync(string roleName)
        {
            Guard.Against.NullInvalid(roleName, Resources.RoleService_FindAsync_The_role_id_is_invalid_);

            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}