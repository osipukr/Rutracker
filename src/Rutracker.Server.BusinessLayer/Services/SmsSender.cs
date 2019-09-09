using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsAuthOptions _smsSettings;

        public SmsSender(IOptions<SmsAuthOptions> smsOptions)
        {
            _smsSettings = smsOptions.Value;
        }

        public async Task SendAsync(string phone, string message)
        {
            TwilioClient.Init(_smsSettings.AccountSid, _smsSettings.AccountToken);

            await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(_smsSettings.AccountFrom),
                to: new PhoneNumber(phone));
        }
    }
}