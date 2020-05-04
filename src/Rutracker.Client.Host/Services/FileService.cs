using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Services.Base;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.Host.Services
{
    public class FileService : Service, IFileService
    {
        private readonly FileOptions _fileOptions;

        public FileService(
            HttpClientService httpClientService,
            IOptions<ApiOptions> apiOptions,
            IOptions<FileOptions> fileOptions) : base(httpClientService, apiOptions)
        {
            _fileOptions = fileOptions.Value;
        }

        public async Task<IEnumerable<FileView>> ListAsync(int torrentId)
        {
            var url = $"{_apiOptions.Files}?{nameof(torrentId)}={torrentId}";

            return await _httpClientService.GetJsonAsync<IEnumerable<FileView>>(url);
        }

        public async Task<FileView> FindAsync(int id)
        {
            var url = string.Format(_apiOptions.File, id.ToString());

            return await _httpClientService.GetJsonAsync<FileView>(url);
        }

        public async Task<FileView> AddAsync(int torrentId, IFileListEntry file)
        {
            if (file.Data.Length > _fileOptions.MaxSize)
            {
                throw new Exception("File must be less than 200 MB.");
            }

            var contents = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.Data);

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.Type);

            contents.Add(fileContent, "file", file.Name);

            var url = string.Format(_apiOptions.FileUpload, torrentId.ToString());

            return await _httpClientService.PostAsync<FileView>(url, contents);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiOptions.File, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}