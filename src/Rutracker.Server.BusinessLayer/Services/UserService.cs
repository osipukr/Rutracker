using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Exceptions;

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
            var users = await _userManager.Users.ToListAsync();

            if (users == null)
            {
                throw new RutrackerException("The users not found.", ExceptionEventType.NotFound);
            }

            return users;
        }

        public async Task<User> FindAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new RutrackerException($"The {nameof(userId)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RutrackerException($"The {nameof(user)} not found.", ExceptionEventType.NotFound);
            }

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new RutrackerException($"The {nameof(user)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }

        public async Task<IEnumerable<string>> RolesAsync(User user)
        {
            if (user == null)
            {
                throw new RutrackerException($"The {nameof(user)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                throw new RutrackerException($"The {nameof(roles)} not found.", ExceptionEventType.NotFound);
            }

            return roles;
        }
    }
}