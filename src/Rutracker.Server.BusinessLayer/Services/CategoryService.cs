using System.Collections.Generic;
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
    public class CategoryService : Service, ICategoryService
    {
        private readonly IDateService _dateService;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IUnitOfWork<RutrackerContext> unitOfWork, IDateService dateService) : base(unitOfWork)
        {
            _dateService = dateService;

            _categoryRepository = _unitOfWork.GetRepository<ICategoryRepository>();
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            var categories = await _categoryRepository.GetAll().AsNoTracking().ToListAsync();

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
            Guard.Against.NullInvalid(category, Resources.Category_Invalid_ErrorMessage);
            Guard.Against.NullString(category.Name, Resources.Category_InvalidName_ErrorMessage);

            if (await _categoryRepository.ExistAsync(x => x.Name == category.Name))
            {
                var message = string.Format(Resources.Category_AlreadyExistsName_ErrorMessage, category.Name);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            category.AddedDate = _dateService.Now();

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            Guard.Against.NullInvalid(category, Resources.Category_Invalid_ErrorMessage);
            Guard.Against.NullString(category.Name, Resources.Category_InvalidName_ErrorMessage);

            if (await _categoryRepository.ExistAsync(x => x.Name == category.Name))
            {
                var message = string.Format(Resources.Category_AlreadyExistsName_ErrorMessage, category.Name);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            var result = await FindAsync(id);

            result.Name = category.Name;
            result.ModifiedDate = _dateService.Now();

            _categoryRepository.Update(result);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await FindAsync(id);

            _categoryRepository.Delete(category);

            await _unitOfWork.SaveChangesAsync();

            return category;
        }
    }
}