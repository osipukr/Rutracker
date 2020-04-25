using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Utils.IdentitySeed.Models;
using Rutracker.Utils.IdentitySeed.Services.Base;

namespace Rutracker.Utils.IdentitySeed.Services
{
    public class RutrackerContextSeed : ContextSeed<RutrackerContext>
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public RutrackerContextSeed(
            RutrackerContext context,
            IUserService userService,
            IRoleService roleService,
            ILogger<RutrackerContextSeed> logger) : base(context, logger)
        {
            _userService = userService;
            _roleService = roleService;
        }

        protected override async Task SeedInternalAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!await _context.Roles.AnyAsync())
            {
                await SeedRolesAsync();
                await _context.SaveChangesAsync();
            }

            if (!await _context.Users.AnyAsync())
            {
                await SeedUsersAsync();
                await _context.SaveChangesAsync();
            }
        }

        private static readonly Role[] RolesList =
        {
            Roles.User,
            Roles.Admin
        };

        private static readonly Tuple<User, string, Role[]>[] UsersList =
        {
            Tuple.Create
            (
                Users.Admin,
                "admin123",
                new []
                {
                    Roles.User,
                    Roles.Admin
                }
            )
        };

        private async Task SeedRolesAsync()
        {
            foreach (var role in RolesList)
            {
                if (!await _roleService.ExistAsync(role.Name))
                {
                    await _roleService.AddAsync(role);
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            foreach (var (user, password, roles) in UsersList)
            {
                if (!await _userService.ExistAsync(user.UserName))
                {
                    await _userService.AddAsync(user, password);

                    foreach (var role in roles)
                    {
                        await _userService.AddToRoleAsync(user.Id, role.Name);
                    }
                }

            }
        }
    }
}