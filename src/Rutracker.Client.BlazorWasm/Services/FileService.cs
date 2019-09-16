using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Client.BlazorWasm.Helpers;
using Rutracker.Client.BlazorWasm.Interfaces;
using Rutracker.Client.BlazorWasm.Settings;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.BlazorWasm.Services
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

        public async Task<FileViewModel> AddAsync(int torrentId, IFileReference file)
        {
            return await _httpClientService.PostTorrentFileAsync<FileViewModel>(_apiUrls.Files, torrentId, file);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrls.File, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}