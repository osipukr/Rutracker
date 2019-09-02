using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrent.Create;

namespace Rutracker.Client.Blazor.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlSettings _apiUrls;

        public CategoryService(HttpClientService httpClientService, ApiUrlSettings apiUrls)
        {
            _httpClientService = httpClientService;
            _apiUrls = apiUrls;
        }

        public async Task<IEnumerable<CategoryViewModel>> ListAsync()
        {
            return await _httpClientService.GetJsonAsync<IEnumerable<CategoryViewModel>>(_apiUrls.Categories);
        }

        public async Task<CategoryViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrls.Category, id.ToString());

            return await _httpClientService.GetJsonAsync<CategoryViewModel>(url);
        }

        public async Task<CategoryViewModel> CreateAsync(CategoryCreateViewModel model)
        {
            return await _httpClientService.PostJsonAsync<CategoryViewModel>(_apiUrls.CategoryAdd, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrls.CategoryDelete, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}