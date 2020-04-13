using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Utils.DatabaseSeed.Interfaces;

namespace Rutracker.Utils.DatabaseSeed.Services.Base
{
    public abstract class ContextSeed<TContext> : IContextSeed where TContext : DbContext
    {
        protected readonly TContext _context;

        protected ContextSeed(TContext context)
        {
            _context = context;
        }

        public abstract Task SeedAsync();
    }
}