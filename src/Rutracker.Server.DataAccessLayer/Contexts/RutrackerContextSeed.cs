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
    public class RutrackerContextSeed
    {
        public static async Task SeedAsync(
            RutrackerContext context,
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

                if (!await context.Forums.AnyAsync())
                {
                    await context.Forums.AddRangeAsync(GetPreconfiguredForums());
                    await context.SaveChangesAsync();
                }

                if (!await context.Torrents.AnyAsync())
                {
                    await context.Torrents.AddRangeAsync(GetPreconfiguredTorrents());
                    await context.SaveChangesAsync();
                }

                if (!await context.Files.AnyAsync())
                {
                    await context.Files.AddRangeAsync(GetPreconfiguredFiles());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                loggerFactory.CreateLogger<RutrackerContextSeed>().LogError(ex.Message);
            }
        }

        #region Generate preconfigured items

        private static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            var roles = new[]
            {
                new Role
                {
                    Name = UserRoles.User
                },
                new Role
                {
                    Name = UserRoles.Admin
                }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            var admin = new User
            {
                UserName = "admin",
                Email = "fredstone624@gmail.com",
                FirstName = "Roman",
                LastName = "Osipuk",
                EmailConfirmed = true,
                ImageUrl = "https://avatars1.githubusercontent.com/u/40744739?s=300&v=4"
            };

            await userManager.CreateAsync(admin, "Admin_Password_123");
            await userManager.AddToRolesAsync(admin, new[]
            {
                UserRoles.User,
                UserRoles.Admin
            });
        }

        private static readonly Random Random = new Random();
        private const int ForumMaxCount = 50;
        private const int TorrentMaxCount = 500;
        private const int FileMaxCount = 1000;

        private static IEnumerable<Forum> GetPreconfiguredForums() =>
            Enumerable.Range(1, ForumMaxCount).Select(id => new Forum
            {
                Id = id,
                Title = Guid.NewGuid().ToString()
            });

        private static IEnumerable<Torrent> GetPreconfiguredTorrents() =>
            Enumerable.Range(1, TorrentMaxCount).Select(id => new Torrent
            {
                Id = id,
                RegisteredAt = DateTime.Now,
                Size = Random.Next(1, int.MaxValue),
                Name = Guid.NewGuid().ToString(),
                Hash = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                TrackerId = Random.Next(1, int.MaxValue),
                ForumId = Random.Next(1, ForumMaxCount)
            });

        private static IEnumerable<File> GetPreconfiguredFiles() =>
            Enumerable.Range(1, FileMaxCount).Select(id => new File
            {
                Name = Guid.NewGuid().ToString(),
                Size = Random.Next(1, int.MaxValue),
                TorrentId = Random.Next(1, TorrentMaxCount)
            });

        #endregion
    }
}