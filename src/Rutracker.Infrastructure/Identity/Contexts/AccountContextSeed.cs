using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Core.Entities.Identity;

namespace Rutracker.Infrastructure.Identity.Contexts
{
    public class AccountContextSeed
    {
        private static readonly IEnumerable<string> Roles;
        private static readonly User AdminUser;
        private static readonly string AdminPassword;

        static AccountContextSeed()
        {
            Roles = new[]
            {
                UserRoles.User,
                UserRoles.Admin
            };

            AdminUser = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin"
            };

            AdminPassword = "Admin_Password_123";
        }

        public static async Task SeedAsync(
            AccountContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                if (!await context.Roles.AnyAsync())
                {
                    await SeedRolesAsync(roleManager);
                    await context.SaveChangesAsync();
                }

                if (!await context.Users.AnyAsync())
                {
                    await SeedUsersAsync(userManager);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                loggerFactory.CreateLogger<AccountContextSeed>().LogError(ex.Message);
            }
        }

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            foreach (var role in Roles)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = role
                });
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            var result = await userManager.CreateAsync(AdminUser, AdminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(AdminUser, Roles);
            }
        }
    }
}