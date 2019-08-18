using System;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class SmsService : ISmsService
    {
        private readonly ISmsSender _smsSender;

        public SmsService(ISmsSender smsSender)
        {
            _smsSender = smsSender ?? throw new ArgumentNullException(nameof(smsSender));
        }

        public async Task SendConfirmationPhoneAsync(string phone, string code)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new RutrackerException("Not valid email.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new RutrackerException("Not valid code.", ExceptionEventType.NotValidParameters);
            }

            await _smsSender.SendAsync(phone, $"Confirmation code: {code}");
        }
    }
}