using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Settings;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlOptions _apiUrlOptions;

        public SubcategoryService(HttpClientService httpClientService, ApiUrlOptions apiUrlOptions)
        {
            _httpClientService = httpClientService;
            _apiUrlOptions = apiUrlOptions;
        }

        public async Task<IEnumerable<SubcategoryViewModel>> ListAsync(int categoryId)
        {
            var url = string.Format(_apiUrlOptions.SubcategoriesSearch, categoryId.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<SubcategoryViewModel>>(url);
        }

        public async Task<SubcategoryViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.Subcategory, id.ToString());

            return await _httpClientService.GetJsonAsync<SubcategoryViewModel>(url);
        }

        public async Task<SubcategoryViewModel> CreateAsync(SubcategoryCreateViewModel model)
        {
            return await _httpClientService.PostJsonAsync<SubcategoryViewModel>(_apiUrlOptions.Subcategories, model);
        }

        public async Task<SubcategoryViewModel> UpdateAsync(int id, SubcategoryUpdateViewModel model)
        {
            var url = string.Format(_apiUrlOptions.Subcategory, id.ToString());

            return await _httpClientService.PutJsonAsync<SubcategoryViewModel>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.Subcategory, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}