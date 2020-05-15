using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Services.Base;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Client.Host.Services
{
    public class SubcategoryService : Service, ISubcategoryService
    {
        public SubcategoryService(HttpClientService httpClientService, IOptions<ApiOptions> apiOptions) : base(httpClientService, apiOptions)
        {
        }

        public async Task<IEnumerable<SubcategoryView>> ListAsync(int? categoryId)
        {
            var url = string.Format(_apiOptions.Subcategories, categoryId?.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<SubcategoryView>>(url);
        }

        public async Task<SubcategoryView> FindAsync(int id)
        {
            var url = string.Format(_apiOptions.Subcategory, id.ToString());

            return await _httpClientService.GetJsonAsync<SubcategoryView>(url);
        }

        public async Task<SubcategoryView> AddAsync(SubcategoryCreateView model)
        {
            return await _httpClientService.PostJsonAsync<SubcategoryView>(_apiOptions.Subcategories, model);
        }

        public async Task<SubcategoryView> UpdateAsync(int id, SubcategoryUpdateView model)
        {
            var url = string.Format(_apiOptions.Subcategory, id.ToString());

            return await _httpClientService.PutJsonAsync<SubcategoryView>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiOptions.Subcategory, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}