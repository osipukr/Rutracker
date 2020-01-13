using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;
using Rutracker.Server.BusinessLayer.Properties;
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
            Guard.Against.IsValidPhoneNumber(phone, Resources.SmsSender_InvalidPhoneNumber);
            Guard.Against.NullString(message, Resources.SmsSender_InvalidMessage);

            TwilioClient.Init(_smsSettings.AccountSid, _smsSettings.AccountToken);

            await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(_smsSettings.AccountFrom),
                to: new PhoneNumber(phone));
        }
    }
}