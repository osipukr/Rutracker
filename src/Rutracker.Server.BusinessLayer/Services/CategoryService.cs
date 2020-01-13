using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();

            Guard.Against.NullNotFound(categories, Resources.Category_NotFound_ErrorMessage);

            return categories;
        }

        public async Task<Category> FindAsync(int id)
        {
            Guard.Against.LessOne(id, Resources.Category_InvalidId_ErrorMessage);

            var category = await _categoryRepository.GetAsync(id);

            Guard.Against.NullNotFound(category, string.Format(Resources.Category_NotFoundById_ErrorMessage, id));

            return category;
        }

        public async Task<Category> AddAsync(Category category)
        {
            Guard.Against.NullNotValid(category, Resources.Category_Invalid_ErrorMessage);
            Guard.Against.NullString(category.Name, Resources.Category_InvalidName_ErrorMessage);

            if (await _categoryRepository.ExistAsync(x => x.Name == category.Name))
            {
                var message = string.Format(Resources.Category_AlreadyExistsName_ErrorMessage, category.Name);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            category.AddedDate = DateTime.UtcNow;

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            Guard.Against.NullNotValid(category, Resources.Category_Invalid_ErrorMessage);
            Guard.Against.NullString(category.Name, Resources.Category_InvalidName_ErrorMessage);

            if (await _categoryRepository.ExistAsync(x => x.Name == category.Name))
            {
                var message = string.Format(Resources.Category_AlreadyExistsName_ErrorMessage, category.Name);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            var result = await FindAsync(id);

            result.Name = category.Name;
            result.ModifiedDate = DateTime.UtcNow;

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