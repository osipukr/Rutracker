using System.Threading.Tasks;
using Ardalis.GuardClauses;
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
        private readonly SmsSettings _smsSettings;

        public SmsSender(IOptions<SmsSettings> smsOptions)
        {
            _smsSettings = smsOptions.Value;

            Guard.Against.Null(_smsSettings, nameof(_smsSettings));
            Guard.Against.NullOrWhiteSpace(_smsSettings.AccountSid, parameterName: nameof(_smsSettings.AccountSid));
            Guard.Against.NullOrWhiteSpace(_smsSettings.AccountFrom, parameterName: nameof(_smsSettings.AccountFrom));
            Guard.Against.NullOrWhiteSpace(_smsSettings.AccountToken, parameterName: nameof(_smsSettings.AccountToken));
        }

        public async Task SendAsync(string phone, string message)
        {
            TwilioClient.Init(_smsSettings.AccountSid, _smsSettings.AccountToken);

            var to = new PhoneNumber(phone.StartsWith("+") ? phone : $"+{phone}");

            await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(_smsSettings.AccountFrom),
                to: to);
        }
    }
}