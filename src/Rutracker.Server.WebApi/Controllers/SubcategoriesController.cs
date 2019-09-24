using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize(Policy = Policies.IsAdmin)]
    public class SubcategoriesController : BaseApiController
    {
        private readonly ISubcategoryService _subcategoryService;

        public SubcategoriesController(ISubcategoryService subcategoryService, IMapper mapper) : base(mapper)
        {
            _subcategoryService = subcategoryService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IEnumerable<SubcategoryViewModel>> List()
        {
            var subcategories = await _subcategoryService.ListAsync();

            return _mapper.Map<IEnumerable<SubcategoryViewModel>>(subcategories);
        }

        [HttpGet("search"), AllowAnonymous]
        public async Task<IEnumerable<SubcategoryViewModel>> Search(int categoryId)
        {
            var subcategories = await _subcategoryService.ListAsync(categoryId);

            return _mapper.Map<IEnumerable<SubcategoryViewModel>>(subcategories);
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<SubcategoryViewModel> Find(int id)
        {
            var subcategory = await _subcategoryService.FindAsync(id);

            return _mapper.Map<SubcategoryViewModel>(subcategory);
        }

        [HttpPost]
        public async Task<SubcategoryViewModel> Create(SubcategoryCreateViewModel model)
        {
            var subcategory = _mapper.Map<Subcategory>(model);
            var result = await _subcategoryService.AddAsync(subcategory);

            return _mapper.Map<SubcategoryViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<SubcategoryViewModel> Update(int id, SubcategoryUpdateViewModel model)
        {
            var subcategory = _mapper.Map<Subcategory>(model);
            var result = await _subcategoryService.UpdateAsync(id, subcategory);

            return _mapper.Map<SubcategoryViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _subcategoryService.DeleteAsync(id);
        }
    }
}