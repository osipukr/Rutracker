using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class SubcategoryService : Service, ISubcategoryService
    {
        private readonly IDateService _dateService;

        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public SubcategoryService(IUnitOfWork<RutrackerContext> unitOfWork, IDateService dateService) : base(unitOfWork)
        {
            _dateService = dateService;

            _categoryRepository = _unitOfWork.GetRepository<ICategoryRepository>();
            _subcategoryRepository = _unitOfWork.GetRepository<ISubcategoryRepository>();
        }

        public async Task<IEnumerable<Subcategory>> ListAsync(int? categoryId)
        {
            Expression<Func<Subcategory, bool>> predicate = null;

            if (categoryId.HasValue)
            {
                Guard.Against.LessOne(categoryId.Value, Resources.Category_InvalidId_ErrorMessage);

                if (!await _categoryRepository.ExistAsync(categoryId.Value))
                {
                    throw new RutrackerException(
                        string.Format(Resources.Category_NotFoundById_ErrorMessage, categoryId),
                        ExceptionEventTypes.InvalidParameters);
                }

                predicate = x => x.CategoryId == categoryId;
            }

            var subcategories = await _subcategoryRepository.GetAll(predicate).AsNoTracking().ToListAsync();

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
            Guard.Against.NullInvalid(subcategory, Resources.Subcategory_Invalid_ErrorMessage);
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

            subcategory.AddedDate = _dateService.Now();

            await _subcategoryRepository.AddAsync(subcategory);
            await _unitOfWork.SaveChangesAsync();

            return subcategory;
        }

        public async Task<Subcategory> UpdateAsync(int id, Subcategory subcategory)
        {
            Guard.Against.NullInvalid(subcategory, Resources.Subcategory_Invalid_ErrorMessage);
            Guard.Against.NullString(subcategory.Name, Resources.Subcategory_InvalidName_ErrorMessage);

            var result = await FindAsync(id);

            if (result.Name != subcategory.Name &&
                await _subcategoryRepository.ExistAsync(x => x.Name == subcategory.Name))
            {
                var message = string.Format(Resources.Subcategory_AlreadyExistsName_ErrorMessage, subcategory.Name);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            result.Name = subcategory.Name;
            result.Description = subcategory.Description;
            result.ModifiedDate = _dateService.Now();

            _subcategoryRepository.Update(result);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<Subcategory> DeleteAsync(int id)
        {
            var subcategory = await FindAsync(id);

            _subcategoryRepository.Delete(subcategory);

            await _unitOfWork.SaveChangesAsync();

            return subcategory;
        }
    }
}