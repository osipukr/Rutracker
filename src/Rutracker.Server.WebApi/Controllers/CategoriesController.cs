using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models.ViewModels.Torrent;

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

        [HttpGet(nameof(Find))]
        public async Task<CategoryViewModel> Find(int id)
        {
            var category = await _categoryService.FindAsync(id);

            return _mapper.Map<CategoryViewModel>(category);
        }
    }
}