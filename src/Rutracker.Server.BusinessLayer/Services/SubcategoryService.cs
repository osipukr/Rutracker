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

        public SubcategoryService(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<IEnumerable<Subcategory>> ListAsync(int categoryId)
        {
            Guard.Against.LessOne(categoryId, $"The {nameof(categoryId)} is less than 1.");

            var subcategories = await _subcategoryRepository.GetAll(x => x.CategoryId == categoryId).ToListAsync();

            Guard.Against.NullNotFound(subcategories, "Subcategories not found.");

            return subcategories;
        }

        public async Task<Subcategory> FindAsync(int subcategoryId)
        {
            Guard.Against.LessOne(subcategoryId, $"The {nameof(subcategoryId)} is less than 1.");

            var subcategory = await _subcategoryRepository.GetAsync(subcategoryId);

            Guard.Against.NullNotFound(subcategory, "Subcategory not found.");

            return subcategory;
        }
    }
}