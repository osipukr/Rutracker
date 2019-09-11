using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize(Policy = Policies.IsAdmin)]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService, IMapper mapper) : base(mapper)
        {
            _categoryService = categoryService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IEnumerable<CategoryViewModel>> List()
        {
            var categories = await _categoryService.ListAsync();

            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<CategoryViewModel> Find(int id)
        {
            var category = await _categoryService.FindAsync(id);

            return _mapper.Map<CategoryViewModel>(category);
        }

        [HttpPost]
        public async Task<CategoryViewModel> Create(CategoryCreateViewModel model)
        {
            var category = _mapper.Map<Category>(model);
            var result = await _categoryService.AddAsync(category);

            return _mapper.Map<CategoryViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<CategoryViewModel> Update(int id, CategoryUpdateViewModel model)
        {
            var category = _mapper.Map<Category>(model);
            var result = await _categoryService.UpdateAsync(id, category);

            return _mapper.Map<CategoryViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
        }
    }
}