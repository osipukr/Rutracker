using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Entities;

namespace Rutracker.Server.DataAccessLayer.Contexts
{
    public class RutrackerContextSeed : IContextSeed
    {
        private readonly RutrackerContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RutrackerContextSeed> _logger;

        public RutrackerContextSeed(
            RutrackerContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILogger<RutrackerContextSeed> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                if (!await _context.Database.EnsureCreatedAsync())
                {
                    return;
                }

                if (!await _context.Roles.AnyAsync())
                {
                    await SeedRolesAsync(_roleManager);
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Users.AnyAsync())
                {
                    await SeedUsersAsync(_userManager);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private static readonly User Admin = new User
        {
            UserName = "admin",
            Email = "fredstone624@gmail.com",
            PhoneNumber = "+375336246410",
            FirstName = "Roman",
            LastName = "Osipuk",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = true,
            ImageUrl = "https://avatars1.githubusercontent.com/u/40744739?s=300&v=4",
            AddedDate = DateTime.UtcNow
        };

        private const string AdminPassword = "Admin_Password_123";

        private static readonly IEnumerable<Role> Roles = new[]
        {
            new Role { Name = UserRoles.User },
            new Role { Name = UserRoles.Admin }
        };

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            foreach (var role in Roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            await userManager.CreateAsync(Admin, AdminPassword);
            await userManager.AddToRolesAsync(Admin, Roles.Select(x => x.Name));
        }
    }
}