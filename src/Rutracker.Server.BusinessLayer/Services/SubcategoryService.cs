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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubcategoryService(ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subcategory>> ListAsync()
        {
            var subcategories = await _subcategoryRepository.GetAll().ToListAsync();

            Guard.Against.NullNotFound(subcategories, "The subcategories not found.");

            return subcategories;
        }

        public async Task<IEnumerable<Subcategory>> ListAsync(int categoryId)
        {
            Guard.Against.LessOne(categoryId, "Invalid category id.");

            var subcategories = await _subcategoryRepository.GetAll(x => x.CategoryId == categoryId).ToListAsync();

            Guard.Against.NullNotFound(subcategories, $"The subcategories with category id '{categoryId}' not found.");

            return subcategories;
        }

        public async Task<Subcategory> FindAsync(int id)
        {
            Guard.Against.LessOne(id, "Invalid subcategory id.");

            var subcategory = await _subcategoryRepository.GetAsync(id);

            Guard.Against.NullNotFound(subcategory, $"The subcategory with id '{id}' not found.");

            return subcategory;
        }

        public async Task<Subcategory> AddAsync(Subcategory subcategory)
        {
            Guard.Against.NullNotValid(subcategory, "Invalid subcategory.");
            Guard.Against.NullOrWhiteSpace(subcategory.Name, message: "The subcategory must contain a name.");
            Guard.Against.LessOne(subcategory.CategoryId, "Invalid category id.");

            if (!await _categoryRepository.ExistAsync(subcategory.CategoryId))
            {
                throw new RutrackerException($"The category with id '{subcategory.CategoryId}' not found.", ExceptionEventTypes.NotValidParameters);
            }

            if (await _subcategoryRepository.ExistAsync(x => x.Name == subcategory.Name))
            {
                throw new RutrackerException($"Subcategory with name '{subcategory.Name}' already exists.", ExceptionEventTypes.NotValidParameters);
            }

            subcategory = await _subcategoryRepository.AddAsync(subcategory);
            await _unitOfWork.CompleteAsync();

            return subcategory;
        }

        public async Task<Subcategory> UpdateAsync(int id, Subcategory subcategory)
        {
            Guard.Against.NullNotValid(subcategory, "Invalid subcategory.");
            Guard.Against.NullOrWhiteSpace(subcategory.Name, message: "The subcategory must contain a name.");

            if (await _subcategoryRepository.ExistAsync(x => x.Name == subcategory.Name))
            {
                throw new RutrackerException($"Subcategory with name '{subcategory.Name}' already exists.", ExceptionEventTypes.NotValidParameters);
            }

            var result = await FindAsync(id);

            result.Name = subcategory.Name;

            result = _subcategoryRepository.Update(result);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Subcategory> DeleteAsync(int id)
        {
            var subcategory = await FindAsync(id);

            subcategory = _subcategoryRepository.Remove(subcategory);

            await _unitOfWork.CompleteAsync();

            return subcategory;
        }
    }
}