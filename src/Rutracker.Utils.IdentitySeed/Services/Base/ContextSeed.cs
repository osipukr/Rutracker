using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rutracker.Utils.IdentitySeed.Interfaces;

namespace Rutracker.Utils.IdentitySeed.Services.Base
{
    public abstract class ContextSeed<TContext> : IContextSeed where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly ILogger _logger;

        protected ContextSeed(TContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task SeedAsync()
        {
            try
            {
                await SeedInternalAsync();
            }
            catch (Exception exception)
            {
                _logger.LogInformation("Cannot seed context of type: {0}", typeof(TContext));
                _logger.LogError(exception, exception.Message);
            }
        }

        protected abstract Task SeedInternalAsync();
    }
}