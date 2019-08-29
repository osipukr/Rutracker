using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();

            if (categories == null)
            {
                throw new RutrackerException("Categories not found.", ExceptionEventType.NotFound);
            }

            return categories;
        }

        public async Task<Category> FindAsync(int categoryId)
        {
            if (categoryId < 1)
            {
                throw new RutrackerException($"The {nameof(categoryId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            var category = await _categoryRepository.GetAsync(categoryId);

            if (category == null)
            {
                throw new RutrackerException("Category not found.", ExceptionEventType.NotFound);
            }

            return category;
        }
    }
}