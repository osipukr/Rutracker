using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

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

            Guard.Against.NullNotFound(categories, "Categories not found.");

            return categories;
        }

        public async Task<Category> FindAsync(int categoryId)
        {
            Guard.Against.LessOne(categoryId, $"The {nameof(categoryId)} is less than 1.");

            var category = await _categoryRepository.GetAsync(categoryId);

            Guard.Against.NullNotFound(category, "Category not found.");

            return category;
        }
    }
}