using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Client.BusinessLayer.Interfaces;
using Rutracker.Client.BusinessLayer.Settings;
using Rutracker.Shared.Models.ViewModels.Message;

namespace Rutracker.Client.BusinessLayer.Services
{
    public class MessageService : IMessageService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlOptions _apiUrlOptions;

        public MessageService(HttpClientService httpClientService, ApiUrlOptions apiUrlOptions)
        {
            _httpClientService = httpClientService;
            _apiUrlOptions = apiUrlOptions;
        }

        public async Task<IEnumerable<MessageViewModel>> ListAsync(int dialogId)
        {
            var url = string.Format(_apiUrlOptions.MessagesSearch, dialogId.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<MessageViewModel>>(url);
        }

        public async Task<MessageViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrlOptions.Message, id.ToString());

            return await _httpClientService.GetJsonAsync<MessageViewModel>(url);
        }
    }
}