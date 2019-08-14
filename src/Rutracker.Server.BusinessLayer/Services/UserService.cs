using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            var users = await Task.FromResult(_userManager.Users.ToList());

            if (users == null)
            {
                throw new TorrentException("The users not found.", ExceptionEventType.NotFound);
            }

            return users;
        }

        public async Task<User> FindAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new TorrentException($"The {nameof(userId)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new TorrentException($"The {nameof(user)} not found.", ExceptionEventType.NotFound);
            }

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new TorrentException($"The {nameof(user)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new TorrentException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }

        public async Task<IEnumerable<string>> RolesAsync(User user)
        {
            if (user == null)
            {
                throw new TorrentException($"The {nameof(user)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                throw new TorrentException($"The {nameof(roles)} not found.", ExceptionEventType.NotFound);
            }

            return roles;
        }
    }
}