using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Entities;

namespace Rutracker.Utils.DatabaseSeed.Models
{
    public static class Roles
    {
        public static readonly Role User =
            new Role
            {
                Name = StockRoles.User,
                IsStockRole = true
            };

        public static readonly Role Admin =
            new Role
            {
                Name = StockRoles.Admin,
                IsStockRole = true
            };
    }
}