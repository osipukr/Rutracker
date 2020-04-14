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
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = Policies.IsAdmin)]
    public class SubcategoriesController : ApiController
    {
        private readonly ISubcategoryService _subcategoryService;

        public SubcategoriesController(ISubcategoryService subcategoryService, IMapper mapper) : base(mapper)
        {
            _subcategoryService = subcategoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<SubcategoryView>> Get(int? categoryId)
        {
            var subcategories = await _subcategoryService.ListAsync(categoryId);

            return _mapper.Map<IEnumerable<SubcategoryView>>(subcategories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<SubcategoryDetailView> Get(int id)
        {
            var subcategory = await _subcategoryService.FindAsync(id);

            return _mapper.Map<SubcategoryDetailView>(subcategory);
        }

        [HttpPost]
        public async Task<SubcategoryView> Post(SubcategoryCreateView model)
        {
            var subcategory = _mapper.Map<Subcategory>(model);

            var result = await _subcategoryService.AddAsync(subcategory);

            return _mapper.Map<SubcategoryView>(result);
        }

        [HttpPut("{id}")]
        public async Task<SubcategoryView> Put(int id, SubcategoryUpdateView model)
        {
            var subcategory = _mapper.Map<Subcategory>(model);

            var result = await _subcategoryService.UpdateAsync(id, subcategory);

            return _mapper.Map<SubcategoryView>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _subcategoryService.DeleteAsync(id);
        }
    }
}