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
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();

            Guard.Against.NullNotFound(categories, "The categories not found.");

            return categories;
        }

        public async Task<Category> FindAsync(int id)
        {
            Guard.Against.LessOne(id, $"The {nameof(id)} is less than 1.");

            var category = await _categoryRepository.GetAsync(id);

            Guard.Against.NullNotFound(category, $"The category with id '{id}' not found.");

            return category;
        }

        public async Task<Category> AddAsync(Category category)
        {
            ThrowIfInvalidCategoryModel(category);

            category = await _categoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            ThrowIfInvalidCategoryModel(category);

            var result = await FindAsync(id);

            result.Name = category.Name;

            result = _categoryRepository.Update(result);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await FindAsync(id);

            category = _categoryRepository.Remove(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }

        private static void ThrowIfInvalidCategoryModel(Category category)
        {
            Guard.Against.NullNotValid(category, "Invalid category model.");
            Guard.Against.NullOrWhiteSpace(category.Name, message: "The category must contain a name.");
        }
    }
}