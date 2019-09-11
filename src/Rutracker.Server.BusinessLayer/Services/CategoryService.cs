using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
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
            Guard.Against.LessOne(id, "Invalid category id.");

            var category = await _categoryRepository.GetAsync(id);

            Guard.Against.NullNotFound(category, $"The category with id '{id}' not found.");

            return category;
        }

        public async Task<Category> AddAsync(Category category)
        {
            Guard.Against.NullNotValid(category, "Invalid category.");
            Guard.Against.NullOrWhiteSpace(category.Name, message: "The category must contain a name.");

            if (await _categoryRepository.ExistAsync(x => x.Name == category.Name))
            {
                throw new RutrackerException($"Category with name '{category.Name}' already exists.", ExceptionEventTypes.NotValidParameters);
            }

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            Guard.Against.NullNotValid(category, "Invalid category.");
            Guard.Against.NullOrWhiteSpace(category.Name, message: "The category must contain a name.");

            if (await _categoryRepository.ExistAsync(x => x.Name == category.Name))
            {
                throw new RutrackerException($"Category with name '{category.Name}' already exists.", ExceptionEventTypes.NotValidParameters);
            }

            var result = await FindAsync(id);

            result.Name = category.Name;

            _categoryRepository.Update(result);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await FindAsync(id);

            _categoryRepository.Remove(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }
    }
}