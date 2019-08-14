using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Contexts
{
    public class IdentityContextSeed
    {
        private static readonly IEnumerable<Role> Roles;
        private static readonly User AdminUser;
        private static readonly string AdminPassword;

        static IdentityContextSeed()
        {
            Roles = new[]
            {
                new Role
                {
                    Name = UserRoles.Names.User,
                    Description = UserRoles.Descriptions.User
                },
                new Role
                {
                    Name = UserRoles.Names.Admin,
                    Description = UserRoles.Descriptions.Admin
                }
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
            IdentityContext context,
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
                loggerFactory.CreateLogger<IdentityContextSeed>().LogError(ex.Message);
            }
        }

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            foreach (var role in Roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            await userManager.CreateAsync(AdminUser, AdminPassword);

            var roleNames = Roles.Select(x => x.Name);

            await userManager.AddToRolesAsync(AdminUser, roleNames);
        }
    }
}