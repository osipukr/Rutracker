using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.HttpClient;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Options;
using Rutracker.Client.BusinessLayer.Services.Base;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class SubcategoryService : Service, ISubcategoryService
    {
        public SubcategoryService(HttpClientService httpClientService, ApiOptions apiOptions) : base(httpClientService, apiOptions)
        {
        }

        public async Task<IEnumerable<SubcategoryView>> ListAsync(int? categoryId)
        {
            var url = string.Format(_apiOptions.Subcategories, categoryId?.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<SubcategoryView>>(url);
        }

        public async Task<SubcategoryDetailView> FindAsync(int id)
        {
            var url = string.Format(_apiOptions.Subcategory, id.ToString());

            return await _httpClientService.GetJsonAsync<SubcategoryDetailView>(url);
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