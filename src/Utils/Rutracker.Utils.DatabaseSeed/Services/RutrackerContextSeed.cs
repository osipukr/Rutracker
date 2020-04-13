using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Entities;
using Rutracker.Utils.DatabaseSeed.Services.Base;

namespace Rutracker.Utils.DatabaseSeed.Services
{
    public class RutrackerContextSeed : ContextSeed<RutrackerContext>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RutrackerContextSeed(RutrackerContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
            : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task SeedAsync()
        {
            if (await _context.Database.EnsureCreatedAsync())
            {
                if (!await _context.Roles.AnyAsync())
                {
                    await SeedRolesAsync(_roleManager);
                }

                if (!await _context.Users.AnyAsync())
                {
                    await SeedUsersAsync(_userManager);
                }

                await _context.SaveChangesAsync();
            }
        }

        private static readonly Tuple<User, string, string[]>[] Users =
        {
            Tuple.Create
            (
                new User
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
                },
                "Admin_Password_123",
                new[]
                {
                    StockRoles.User,
                    StockRoles.Admin
                }
            )
        };

        private static readonly Role[] Roles =
        {
            new Role
            {
                Name = StockRoles.User,
                IsStockRole = true
            },

            new Role
            {
                Name = StockRoles.Admin,
                IsStockRole = true
            }
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
            foreach (var (user, password, roles) in Users)
            {
                await userManager.CreateAsync(user, password);
                await userManager.AddToRolesAsync(user, roles);
            }
        }
    }
}