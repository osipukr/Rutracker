using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.Blazor.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlSettings _apiUrls;

        public FileService(HttpClientService httpClientService, ApiUrlSettings apiUrls)
        {
            _httpClientService = httpClientService;
            _apiUrls = apiUrls;
        }

        public async Task<IEnumerable<FileViewModel>> ListAsync(int torrentId)
        {
            var url = string.Format(_apiUrls.FilesSearch, torrentId.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<FileViewModel>>(url);
        }

        public async Task<FileViewModel> CreateAsync(FileCreateViewModel model)
        {
            return await _httpClientService.PostJsonAsync<FileViewModel>(_apiUrls.Files, model);
        }

        public async Task<FileViewModel> Update(int id, FileUpdateViewModel model)
        {
            var url = string.Format(_apiUrls.File, id.ToString());

            return await _httpClientService.PutJsonAsync<FileViewModel>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrls.File, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}