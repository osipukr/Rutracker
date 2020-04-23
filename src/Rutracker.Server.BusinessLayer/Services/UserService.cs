using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class UserService : Service, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;
        private readonly IDateService _dateService;

        public UserService(UserManager<User> userManager, IRoleService roleService, IDateService dateService)
        {
            _userManager = userManager;
            _roleService = roleService;
            _dateService = dateService;
        }

        public async Task<IPagedList<User>> ListAsync(IUserFilter filter)
        {
            Guard.Against.OutOfRange(filter.Page, Constants.Filter.PageRangeFrom, Constants.Filter.PageRangeTo, Resources.Page_InvalidPageNumber);
            Guard.Against.OutOfRange(filter.PageSize, Constants.Filter.PageSizeRangeFrom, Constants.Filter.PageSizeRangeTo, Resources.PageSize_InvalidPageSizeNumber);

            var query = _userManager.Users.OrderBy(x => x.AddedDate).AsNoTracking();

            var pagedList = await ApplyFilterAsync(query, filter);

            Guard.Against.NullNotFound(pagedList.Items, Resources.User_NotFoundList_ErrorMessage);

            return pagedList;
        }

        public async Task<User> AddAsync(User user)
        {
            Guard.Against.NullInvalid(user, Resources.User_Invalid_ErrorMessage);

            var result = await _userManager.CreateAsync(user);

            Guard.Against.IsSucceeded(result);

            return user;
        }

        public async Task<User> AddAsync(User user, string password)
        {
            Guard.Against.NullInvalid(user, Resources.User_Invalid_ErrorMessage);

            var result = await _userManager.CreateAsync(user, password);

            Guard.Against.IsSucceeded(result);

            return user;
        }

        public async Task<User> AddToRoleAsync(string userId, string roleName)
        {
            var user = await FindAsync(userId);

            if (!await _roleService.ExistAsync(roleName))
            {
                throw new RutrackerException(
                    Resources.UserService_AddToRoleAsync_The_role_name_is_invalid_,
                    ExceptionEventTypes.InvalidParameters);
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            Guard.Against.IsSucceeded(result);

            return user;
        }

        public async Task<User> FindAsync(string userId)
        {
            Guard.Against.NullString(userId, Resources.User_InvalidId_ErrorMessage);

            var user = await _userManager.FindByIdAsync(userId);

            Guard.Against.NullNotFound(user, string.Format(Resources.User_NotFoundById_ErrorMessage, userId));

            return user;
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            Guard.Against.NullString(userName, Resources.User_InvalidUserName_ErrorMessage);

            var user = await _userManager.FindByNameAsync(userName);

            Guard.Against.NullNotFound(user, string.Format(Resources.User_NotFoundByName_ErrorMessage, userName));

            return user;
        }

        public async Task<bool> ExistAsync(string userName)
        {
            Guard.Against.NullString(userName, Resources.User_InvalidUserName_ErrorMessage);

            return await _userManager.Users.AnyAsync(user => user.UserName == userName);
        }

        public async Task<User> UpdateAsync(string userId, User user)
        {
            Guard.Against.NullString(userId, Resources.User_InvalidId_ErrorMessage);
            Guard.Against.NullInvalid(user, Resources.User_Invalid_ErrorMessage);

            var result = await FindAsync(userId);

            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.ModifiedDate = _dateService.Now();

            var updateResult = await _userManager.UpdateAsync(result);

            Guard.Against.IsSucceeded(updateResult);

            return result;
        }

        public async Task<IEnumerable<Role>> RolesAsync(string userId)
        {
            var user = await FindAsync(userId);
            var roleNames = await _userManager.GetRolesAsync(user);

            Guard.Against.NullNotFound(roleNames, Resources.Role_NotFoundList_ErrorMessage);

            var roles = new List<Role>(roleNames.Count);

            foreach (var name in roleNames)
            {
                var role = await _roleService.FindByNameAsync(name);

                roles.Add(role);
            }

            return roles;
        }

        public async Task<string> PasswordResetTokenAsync(string userId)
        {
            var user = await FindAsync(userId);

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<User> ResetPasswordAsync(string userId, string password, string token)
        {
            var user = await FindAsync(userId);
            var result = await _userManager.ResetPasswordAsync(user, token, password);

            Guard.Against.IsSucceeded(result);

            return user;
        }

        public async Task<User> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            Guard.Against.NullString(oldPassword, Resources.User_InvalidOldPassword_ErrorMessage);
            Guard.Against.NullString(newPassword, Resources.User_InvalidNewPassword_ErrorMessage);

            var user = await FindAsync(userId);

            if (!await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                throw new RutrackerException(
                    Resources.User_WrongPassword_ErrorMessage,
                    ExceptionEventTypes.InvalidParameters);
            }

            if (oldPassword == newPassword)
            {
                throw new RutrackerException(
                    Resources.User_SamePasswords_ErrorMessage,
                    ExceptionEventTypes.InvalidParameters);
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            Guard.Against.IsSucceeded(result);

            return user;
        }
    }
}