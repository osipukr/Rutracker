using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Settings;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlOptions _apiUrlOptions;

        public CategoryService(HttpClientService httpClientService, ApiUrlOptions apiUrlOptions)
        {
            _httpClientService = httpClientService;
            _apiUrlOptions = apiUrlOptions;
        }

        public async Task<IEnumerable<CategoryViewModel>> ListAsync()
        {
            return await _httpClientService.GetJsonAsync<IEnumerable<CategoryViewModel>>(_apiUrlOptions.Categories);
        }

        public async Task<CategoryViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.Category, id.ToString());

            return await _httpClientService.GetJsonAsync<CategoryViewModel>(url);
        }

        public async Task<CategoryViewModel> CreateAsync(CategoryCreateViewModel model)
        {
            return await _httpClientService.PostJsonAsync<CategoryViewModel>(_apiUrlOptions.Categories, model);
        }

        public async Task<CategoryViewModel> UpdateAsync(int id, CategoryUpdateViewModel model)
        {
            var url = string.Format(_apiUrlOptions.Category, id.ToString());

            return await _httpClientService.PutJsonAsync<CategoryViewModel>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.Category, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}