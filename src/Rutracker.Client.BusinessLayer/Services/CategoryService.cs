using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.HttpClient;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Options;
using Rutracker.Client.BusinessLayer.Services.Base;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(HttpClientService httpClientService, ApiOptions apiOptions) : base(httpClientService, apiOptions)
        {
        }

        public async Task<IEnumerable<CategoryView>> ListAsync()
        {
            return await _httpClientService.GetJsonAsync<IEnumerable<CategoryView>>(_apiOptions.Categories);
        }

        public async Task<CategoryDetailView> FindAsync(int id)
        {
            var url = string.Format(_apiOptions.Category, id.ToString());

            return await _httpClientService.GetJsonAsync<CategoryDetailView>(url);
        }

        public async Task<CategoryView> AddAsync(CategoryCreateView model)
        {
            return await _httpClientService.PostJsonAsync<CategoryView>(_apiOptions.Categories, model);
        }

        public async Task<CategoryView> UpdateAsync(int id, CategoryUpdateView model)
        {
            var url = string.Format(_apiOptions.Category, id.ToString());

            return await _httpClientService.PutJsonAsync<CategoryView>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiOptions.Category, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}