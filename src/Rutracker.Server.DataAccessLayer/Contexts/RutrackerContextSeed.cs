using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Entities;

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

                if (!await context.Categories.AnyAsync())
                {
                    await context.Categories.AddRangeAsync(GetPreconfiguredCategories());
                    await context.SaveChangesAsync();
                }

                if (!await context.Subcategories.AnyAsync())
                {
                    await context.Subcategories.AddRangeAsync(GetPreconfiguredSubcategories());
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

                if (!await context.Comments.AnyAsync())
                {
                    await context.Comments.AddRangeAsync(GetPreconfiguredComments());
                    await context.SaveChangesAsync();
                }

                if (!await context.Likes.AnyAsync())
                {
                    await context.Likes.AddRangeAsync(GetPreconfiguredLikes());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                loggerFactory.CreateLogger<RutrackerContextSeed>().LogError(ex.Message);
            }
        }

        #region Generate preconfigured items

        private static readonly User Admin = new User
        {
            UserName = "admin",
            Email = "fredstone624@gmail.com",
            FirstName = "Roman",
            LastName = "Osipuk",
            EmailConfirmed = true,
            ImageUrl = "https://avatars1.githubusercontent.com/u/40744739?s=300&v=4"
        };

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
            await userManager.CreateAsync(Admin, "Admin_Password_123");
            await userManager.AddToRolesAsync(Admin, Roles.Select(x => x.Name));
        }

        private static readonly Random Random = new Random();
        private const int CategoryMaxCount = 10;
        private const int SubcategoryMaxCount = 50;
        private const int TorrentMaxCount = 1000;
        private const int FileMaxCount = 30000;
        private const int CommentMaxCount = 100;
        private static readonly int LikeMaxCount = Random.Next(1, CommentMaxCount);

        private static IEnumerable<Category> GetPreconfiguredCategories() =>
            Enumerable.Range(1, CategoryMaxCount).Select(id => new Category
            {
                Name = Guid.NewGuid().ToString()
            });

        private static IEnumerable<Subcategory> GetPreconfiguredSubcategories() =>
            Enumerable.Range(1, SubcategoryMaxCount).Select(id => new Subcategory
            {
                Name = Guid.NewGuid().ToString(),
                CategoryId = Random.Next(1, CategoryMaxCount)
            });

        private static IEnumerable<Torrent> GetPreconfiguredTorrents() =>
            Enumerable.Range(1, TorrentMaxCount).Select(id => new Torrent
            {
                CreatedAt = DateTime.Now,
                Size = Random.Next(1, int.MaxValue),
                Name = Guid.NewGuid().ToString(),
                Hash = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                SubcategoryId = Random.Next(1, SubcategoryMaxCount),
                UserId = Admin.Id
            });

        private static IEnumerable<File> GetPreconfiguredFiles() =>
            Enumerable.Range(1, FileMaxCount).Select(id => new File
            {
                Name = Guid.NewGuid().ToString(),
                Size = Random.Next(1, int.MaxValue),
                TorrentId = Random.Next(1, TorrentMaxCount)
            });

        private static IEnumerable<Comment> GetPreconfiguredComments() =>
            Enumerable.Range(1, CommentMaxCount).Select(id => new Comment
            {
                Text = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                IsModified = false,
                LastModifiedAt = null,
                TorrentId = Random.Next(1, TorrentMaxCount),
                UserId = Admin.Id
            });

        private static IEnumerable<Like> GetPreconfiguredLikes() =>
            Enumerable.Range(1, LikeMaxCount).Select(id => new Like
            {
                CommentId = id,
                UserId = Admin.Id
            });

        #endregion
    }
}