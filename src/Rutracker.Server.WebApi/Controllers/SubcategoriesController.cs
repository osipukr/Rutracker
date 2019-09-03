using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Server.WebApi.Controllers
{
    public class SubcategoriesController : BaseApiController
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly IMapper _mapper;

        public SubcategoriesController(ISubcategoryService subcategoryService, IMapper mapper)
        {
            _subcategoryService = subcategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SubcategoryViewModel>> List(int categoryId)
        {
            var subcategories = await _subcategoryService.ListAsync(categoryId);

            return _mapper.Map<IEnumerable<SubcategoryViewModel>>(subcategories);
        }

        [HttpGet(nameof(Find))]
        public async Task<SubcategoryViewModel> Find(int id)
        {
            var subcategory = await _subcategoryService.FindAsync(id);

            return _mapper.Map<SubcategoryViewModel>(subcategory);
        }
    }
}