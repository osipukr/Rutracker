using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubcategoryService(ISubcategoryRepository subcategoryRepository, IUnitOfWork unitOfWork)
        {
            _subcategoryRepository = subcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subcategory>> ListAsync(int categoryId)
        {
            Guard.Against.LessOne(categoryId, $"The {nameof(categoryId)} is less than 1.");

            var subcategories = await _subcategoryRepository.GetAll(x => x.CategoryId == categoryId).ToListAsync();

            Guard.Against.NullNotFound(subcategories, "Subcategories not found.");

            return subcategories;
        }

        public async Task<Subcategory> FindAsync(int id)
        {
            Guard.Against.LessOne(id, $"The {nameof(id)} is less than 1.");

            var subcategory = await _subcategoryRepository.GetAsync(id);

            Guard.Against.NullNotFound(subcategory, "Subcategory not found.");

            return subcategory;
        }

        public async Task<Subcategory> AddAsync(Subcategory subcategory)
        {
            ThrowIfInvalidSubcategoryModel(subcategory);

            await _subcategoryRepository.AddAsync(subcategory);
            await _unitOfWork.CompleteAsync();

            return subcategory;
        }

        public async Task<Subcategory> UpdateAsync(int id, Subcategory subcategory)
        {
            ThrowIfInvalidSubcategoryModel(subcategory);

            var result = await FindAsync(id);

            result.Name = subcategory.Name;

            _subcategoryRepository.Update(result);
            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Subcategory> DeleteAsync(int id)
        {
            var subcategory = await FindAsync(id);

            _subcategoryRepository.Remove(subcategory);

            await _unitOfWork.CompleteAsync();

            return subcategory;
        }

        private static void ThrowIfInvalidSubcategoryModel(Subcategory subcategory)
        {
            Guard.Against.NullNotValid(subcategory, $"Not valid {nameof(subcategory)}.");
            Guard.Against.NullOrWhiteSpace(subcategory.Name, message: $"The {nameof(subcategory.Name)} is null or white space.");
        }
    }
}