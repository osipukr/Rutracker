using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Settings;
using Rutracker.Shared.Models.ViewModels.Dialog;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class DialogService : IDialogService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlOptions _apiUrlOptions;

        public DialogService(HttpClientService httpClientService, ApiUrlOptions apiUrlOptions)
        {
            _httpClientService = httpClientService;
            _apiUrlOptions = apiUrlOptions;
        }

        public async Task<IEnumerable<DialogViewModel>> ListAsync()
        {
            return await _httpClientService.GetJsonAsync<IEnumerable<DialogViewModel>>(_apiUrlOptions.Dialogs);
        }

        public async Task<DialogViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.Dialog, id.ToString());

            return await _httpClientService.GetJsonAsync<DialogViewModel>(url);
        }
    }
}