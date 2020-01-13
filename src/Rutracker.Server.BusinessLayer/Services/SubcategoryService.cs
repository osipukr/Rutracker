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
    public class SubcategoryService : BaseService, ISubcategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public SubcategoryService(ISubcategoryRepository subcategoryRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Subcategory>> ListAsync()
        {
            var subcategories = await _subcategoryRepository.GetAll().ToListAsync();

            Guard.Against.NullNotFound(subcategories, Resources.Subcategory_NotFoundList_ErrorMessage);

            return subcategories;
        }

        public async Task<IEnumerable<Subcategory>> ListAsync(int categoryId)
        {
            Guard.Against.LessOne(categoryId, Resources.Category_InvalidId_ErrorMessage);

            if (!await _categoryRepository.ExistAsync(categoryId))
            {
                var message = string.Format(Resources.Category_NotFoundById_ErrorMessage, categoryId);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            var subcategories = await _subcategoryRepository.GetAll(x => x.CategoryId == categoryId).ToListAsync();

            Guard.Against.NullNotFound(subcategories, string.Format(Resources.Subcategory_NotFoundByCategoryId_ErrorMessage, categoryId));

            return subcategories;
        }

        public async Task<Subcategory> FindAsync(int id)
        {
            Guard.Against.LessOne(id, Resources.Subcategory_InvalidId_ErrorMessage);

            var subcategory = await _subcategoryRepository.GetAsync(id);

            Guard.Against.NullNotFound(subcategory, string.Format(Resources.Subcategory_NotFoundById_ErrorMessage, id));

            return subcategory;
        }

        public async Task<Subcategory> AddAsync(Subcategory subcategory)
        {
            Guard.Against.NullNotValid(subcategory, Resources.Subcategory_Invalid_ErrorMessage);
            Guard.Against.NullString(subcategory.Name, Resources.Subcategory_InvalidName_ErrorMessage);
            Guard.Against.LessOne(subcategory.CategoryId, Resources.Category_InvalidId_ErrorMessage);

            if (!await _categoryRepository.ExistAsync(subcategory.CategoryId))
            {
                var message = string.Format(Resources.Category_NotFoundById_ErrorMessage, subcategory.CategoryId);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            if (await _subcategoryRepository.ExistAsync(x => x.Name == subcategory.Name))
            {
                var message = string.Format(Resources.Subcategory_AlreadyExistsName_ErrorMessage, subcategory.Name);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            subcategory.AddedDate = DateTime.UtcNow;

            await _subcategoryRepository.AddAsync(subcategory);
            await _unitOfWork.CompleteAsync();

            return subcategory;
        }

        public async Task<Subcategory> UpdateAsync(int id, Subcategory subcategory)
        {
            Guard.Against.NullNotValid(subcategory, Resources.Subcategory_Invalid_ErrorMessage);
            Guard.Against.NullString(subcategory.Name, Resources.Subcategory_InvalidName_ErrorMessage);

            if (await _subcategoryRepository.ExistAsync(x => x.Name == subcategory.Name))
            {
                var message = string.Format(Resources.Subcategory_AlreadyExistsName_ErrorMessage, subcategory.Name);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            var result = await FindAsync(id);

            result.Name = subcategory.Name;
            result.ModifiedDate = DateTime.UtcNow;

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
    }
}