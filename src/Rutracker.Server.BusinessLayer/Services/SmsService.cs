using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Rutracker.Server.BusinessLayer.Interfaces;

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
            Guard.Against.NullOrWhiteSpace(phone, message: "Invalid phone number.");
            Guard.Against.NullOrWhiteSpace(code, message: "Invalid phone verification code.");

            await _smsSender.SendAsync(phone, $"Confirmation code: {code}");
        }
    }
}