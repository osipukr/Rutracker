using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public SubcategoryService(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<IEnumerable<Subcategory>> ListAsync(int categoryId)
        {
            if (categoryId < 1)
            {
                throw new RutrackerException($"The {nameof(categoryId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            var subcategories = await _subcategoryRepository.GetAll(x => x.CategoryId == categoryId).ToListAsync();

            if (subcategories == null)
            {
                throw new RutrackerException("Subcategories not found.", ExceptionEventType.NotFound);
            }

            return subcategories;
        }

        public async Task<Subcategory> FindAsync(int subcategoryId)
        {
            if (subcategoryId < 1)
            {
                throw new RutrackerException($"The {nameof(subcategoryId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            var subcategory = await _subcategoryRepository.GetAsync(subcategoryId);

            if (subcategory == null)
            {
                throw new RutrackerException("Subcategory not found.", ExceptionEventType.NotFound);
            }

            return subcategory;
        }
    }
}