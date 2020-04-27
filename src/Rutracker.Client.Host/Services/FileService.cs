using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Client.Host.Options;
using Rutracker.Client.Host.Services.Base;
using Rutracker.Client.Infrastructure.Interfaces;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.Host.Services
{
    public class FileService : Service, IFileService
    {
        public FileService(HttpClientService httpClientService, IOptions<ApiOptions> apiOptions) : base(httpClientService, apiOptions)
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