using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Core.Entities.Accounts;

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
                LastName = "Admin",
                PhoneNumber = "77777777"
            };

            AdminPassword = "Admin_Password_123";
        }

        public static async Task SeedAsync(
            AccountContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<AccountContextSeed>();

            try
            {
                if (!await context.Roles.AnyAsync())
                {
                    await SeedRolesAsync(roleManager, logger);
                    await context.SaveChangesAsync();
                }

                if (!await context.Users.AnyAsync())
                {
                    await SeedUsersAsync(userManager, logger);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager, ILogger logger)
        {
            foreach (var role in Roles)
            {
                var result = await roleManager.CreateAsync(new Role
                {
                    Name = role
                });

                if (!result.Succeeded)
                {
                    logger.LogError(message: GetErrorFromResult(result));
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager, ILogger logger)
        {
            var result = await userManager.CreateAsync(AdminUser, AdminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(AdminUser, Roles);
            }
            else
            {
                logger.LogError(message: GetErrorFromResult(result));
            }
        }

        private static string GetErrorFromResult(IdentityResult result) => result.Errors.First().Description;
    }
}