using System;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Utils.DatabaseSeed.Models
{
    public static class Users
    {
        public static readonly User Admin =
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
            };
    }
}