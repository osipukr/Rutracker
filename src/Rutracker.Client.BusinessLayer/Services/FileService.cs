using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Client.BusinessLayer.Extensions;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Settings;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlOptions _apiUrlOptions;
        private readonly FileOptions _fileOptions;

        public FileService(HttpClientService httpClientService, ApiUrlOptions apiUrlOptions, FileOptions fileOptions)
        {
            _httpClientService = httpClientService;
            _apiUrlOptions = apiUrlOptions;
            _fileOptions = fileOptions;
        }

        public async Task<IEnumerable<FileViewModel>> ListAsync(int torrentId)
        {
            var url = string.Format(_apiUrlOptions.FilesSearch, torrentId.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<FileViewModel>>(url);
        }

        public async Task<FileViewModel> AddAsync(int torrentId, IFileReference file)
        {
            var fileInfo = await file.ReadFileInfoAsync();

            if (fileInfo.Size > _fileOptions.MaxFileSize)
            {
                throw new Exception($"File '{fileInfo.Name}' is too large.");
            }

            var stream = await file.OpenReadAsync();

            return await _httpClientService.PostTorrentFileAsync<FileViewModel>(_apiUrlOptions.Files, torrentId, fileInfo.Type, fileInfo.Name, stream);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.File, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }

        public async Task<string> DownloadAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.FileDownload, id.ToString());

            return await _httpClientService.GetJsonAsync<string>(url);
        }
    }
}