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

                if (!await _context.Categories.AnyAsync())
                {
                    await _context.Categories.AddRangeAsync(GetPreconfiguredCategories());
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Subcategories.AnyAsync())
                {
                    await _context.Subcategories.AddRangeAsync(GetPreconfiguredSubcategories());
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Torrents.AnyAsync())
                {
                    await _context.Torrents.AddRangeAsync(GetPreconfiguredTorrents());
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Files.AnyAsync())
                {
                    await _context.Files.AddRangeAsync(GetPreconfiguredFiles());
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Comments.AnyAsync())
                {
                    await _context.Comments.AddRangeAsync(GetPreconfiguredComments());
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Likes.AnyAsync())
                {
                    await _context.Likes.AddRangeAsync(GetPreconfiguredLikes());
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        #region Generate preconfigured items

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
            RegisteredAt = DateTime.UtcNow,
            IsRegistrationFinished = true
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
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                SubcategoryId = Random.Next(1, SubcategoryMaxCount),
                UserId = Admin.Id
            });

        private static IEnumerable<File> GetPreconfiguredFiles() =>
            Enumerable.Range(1, FileMaxCount).Select(id => new File
            {
                Name = Guid.NewGuid().ToString(),
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