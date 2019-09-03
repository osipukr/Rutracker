using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Client.Blazor.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlSettings _apiUriSettings;

        public SubcategoryService(HttpClientService httpClientService, ApiUrlSettings uriSettings)
        {
            _httpClientService = httpClientService;
            _apiUriSettings = uriSettings;
        }

        public async Task<IEnumerable<SubcategoryViewModel>> ListAsync(int categoryId)
        {
            var url = string.Format(_apiUriSettings.Subcategories, categoryId.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<SubcategoryViewModel>>(url);
        }

        public async Task<SubcategoryViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUriSettings.Subcategory, id.ToString());

            return await _httpClientService.GetJsonAsync<SubcategoryViewModel>(url);
        }
    }
}