using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Services.Base;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Client.Host.Services
{
    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(HttpClientService httpClientService, IOptions<ApiOptions> apiOptions) : base(httpClientService, apiOptions)
        {
        }

        public async Task<List<CategoryView>> ListAsync()
        {
            return await _httpClientService.GetJsonAsync<List<CategoryView>>(_apiOptions.Categories);
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