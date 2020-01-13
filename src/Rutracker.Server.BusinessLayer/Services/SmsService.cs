using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class SmsService : ISmsService
    {
        private readonly ISmsSender _smsSender;

        public SmsService(ISmsSender smsSender)
        {
            _smsSender = smsSender;
        }

        public async Task SendConfirmationPhoneAsync(string phone, string code)
        {
            var message = string.Format(Resources.SmsService_ConfirmationCodeMessage, code);

            await _smsSender.SendAsync(phone, message);
        }
    }
}