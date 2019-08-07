using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rutracker.Core.Entities.Accounts;
using Rutracker.Core.Exceptions;
using Rutracker.Core.Interfaces.Services;

namespace Rutracker.Infrastructure.Services
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly UserManager<User> _userManager;

        public EmailConfirmationService(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task ConfirmEmailAsync(string userId, string confirmationCode)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new TorrentException($"User id {userId} not valid.", ExceptionEventType.NotValidParameters);
            }

            if (user.EmailConfirmed)
            {
                throw new TorrentException("Email already confirmed.", ExceptionEventType.EmailAlreadyConfirmed);
            }

            var result = await _userManager.ConfirmEmailAsync(user, confirmationCode);

            if (!result.Succeeded)
            {
                throw new TorrentException("Not valid token.", ExceptionEventType.NotValidParameters);
            }
        }
    }
}