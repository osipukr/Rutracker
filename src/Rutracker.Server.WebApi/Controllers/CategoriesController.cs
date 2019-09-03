using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Server.WebApi.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryViewModel>> List()
        {
            var categories = await _categoryService.ListAsync();

            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        [HttpGet("{id}")]
        public async Task<CategoryViewModel> Find(int id)
        {
            var category = await _categoryService.FindAsync(id);

            return _mapper.Map<CategoryViewModel>(category);
        }

        [HttpPost]
        public async Task<CategoryViewModel> Add(CategoryCreateViewModel model)
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