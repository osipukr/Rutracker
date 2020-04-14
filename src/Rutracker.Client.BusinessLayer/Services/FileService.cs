using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.HttpClient;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Options;
using Rutracker.Client.BusinessLayer.Services.Base;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class FileService : Service, IFileService
    {
        public FileService(HttpClientService httpClientService, ApiOptions apiOptions) : base(httpClientService, apiOptions)
        {
        }

        public async Task<IEnumerable<FileView>> ListAsync(int torrentId)
        {
            var url = string.Format(_apiOptions.Files, torrentId.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<FileView>>(url);
        }

        public async Task<FileView> FindAsync(int id)
        {
            var url = string.Format(_apiOptions.File, id.ToString());

            return await _httpClientService.GetJsonAsync<FileView>(url);
        }
    }
}