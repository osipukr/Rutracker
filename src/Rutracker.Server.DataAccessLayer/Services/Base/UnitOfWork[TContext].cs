using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;

namespace Rutracker.Server.DataAccessLayer.Services.Base
{
    public abstract class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private TContext _context;
        private Dictionary<Type, Type> _repositories;

        protected UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _repositories = new Dictionary<Type, Type>();
        }

        public TContext DbContext => _context;

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            var interfaceType = typeof(TRepository);

            if (_repositories == null || !_repositories.ContainsKey(interfaceType))
            {
                throw new KeyNotFoundException(nameof(TRepository));
            }

            var repositoryType = _repositories[interfaceType];

            var repository = Activator.CreateInstance(repositoryType, _context);

            if (repository == null)
            {
                throw new ArgumentException(null, nameof(TRepository));
            }

            return (TRepository)repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected void AddRepository<TService, TImplementation>() where TImplementation : IRepository, TService
        {
            AddRepository(typeof(TService), typeof(TImplementation));
        }

        protected void AddRepository(Type serviceType, Type implementationType)
        {
            _repositories.Add(serviceType, implementationType);
        }

        #region IDisposable Implementation

        protected bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositories?.Clear();
                    _context.Dispose();

                    _repositories = null;
                    _context = null;
                }
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}