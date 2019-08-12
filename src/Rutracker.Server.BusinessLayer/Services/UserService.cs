using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<IReadOnlyList<User>> GetAllUserAsync()
        {
            var users = await Task.FromResult(_userManager.Users.ToList());

            if (users == null)
            {
                throw new TorrentException("The users not found.", ExceptionEventType.NotFound);
            }

            return users;
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new TorrentException("Not valid claims principal.", ExceptionEventType.NotValidParameters);
            }

            var user = await _userManager.GetUserAsync(principal);

            if (user == null)
            {
                throw new TorrentException("The user not found.", ExceptionEventType.NotFound);
            }

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new TorrentException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new TorrentException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }
    }
}